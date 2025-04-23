using System.Reflection.Emit;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace RemoveScourgeInvaders
{
	[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
	[BepInProcess("Magicite.exe")]
	public class RemoveInvader : BaseUnityPlugin
	{
		public void Awake()
		{
			Harmony.CreateAndPatchAll(typeof(RemoveInvader), "RemoveInvader");
			//Logger.LogInfo("Patches loaded!");
		}

		[HarmonyPatch]
		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> RemoveInvaderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			CodeMatcher codeMatcher = new CodeMatcher(instructions);
			
			int msgInstLen	= 8; // 8 instructions for prior code to nop, 'this.$self_$1519.StartCoroutine_Auto(this.$self_$1519.Write(5));'
			int rpcInstLen	= 7; // 7 instructions for 'GameScript.player.GetComponent<NetworkView>().RPC("Boss", RPCMode.All, new object[0]);'
			int ifInstLen	= 6; // 6 instructions for following code to swap it with, 'if (MenuScript.GameMode == 1 && GameScript.districtLevel < 20)'
			int spnInstLen = 13; // 13 instructions for 'Network.Instantiate(Resources.Load("e/invader"), new Vector3(-15f, 15f, 0f), Quaternion,identity, 0);'

			codeMatcher.MatchForward
			(
				false,
				new CodeMatch(OpCodes.Ldsfld),
				new CodeMatch(OpCodes.Callvirt),
				new CodeMatch(OpCodes.Ldstr, "Boss")
			).Advance(-msgInstLen);

			// disable 'The Scourge have invaded!' message
			for (int i = 0; i < msgInstLen; i++)
			{
				List<Label> msgLabels = codeMatcher.Labels;
				codeMatcher.SetInstruction(new CodeInstruction(OpCodes.Nop))
					.AddLabels(msgLabels)
					.Advance(1);
			}

			// disable Boss music for invader spawns
			List<CodeInstruction> rpcInstructions = new List<CodeInstruction>();
			List<CodeInstruction> ifInstructions = new List<CodeInstruction>();

			for (int i = 0; i < rpcInstLen; i++)
			{
				rpcInstructions.Add(codeMatcher.Instruction);
				codeMatcher.Advance(1);
			}
			for (int i = 0; i < ifInstLen; i++)
			{
				ifInstructions.Add(codeMatcher.Instruction);
				codeMatcher.Advance(1);
			}

			codeMatcher.Advance(-rpcInstLen - ifInstLen);
			foreach (CodeInstruction instruction in ifInstructions)
			{
				codeMatcher.SetInstructionAndAdvance(instruction);
			}
			foreach (CodeInstruction instruction in rpcInstructions)
			{
				codeMatcher.SetInstructionAndAdvance(instruction);
			}
			
			// disable invader spawns
			codeMatcher.MatchForward
			(
				false,
				new CodeMatch(OpCodes.Ldstr, "e/invader")
			);

			for (int i = 0; i < spnInstLen; i++)
			{
				List<Label> curLabels = codeMatcher.Labels;

				codeMatcher.SetInstruction(new CodeInstruction(OpCodes.Nop))
				.AddLabels(curLabels)
				.Advance(1);
			}

			return codeMatcher.Instructions();
		}

		private static MethodInfo TargetMethod()
		{
			string nestedInvaderType = "$Invader$1516";
			string nestedMethod = "MoveNext";

			MethodInfo moveNextMethod = typeof(GameScript).GetNestedType(nestedInvaderType, BindingFlags.NonPublic | BindingFlags.Instance)
				.GetNestedType("$", BindingFlags.NonPublic | BindingFlags.Instance)
				.GetMethod(nestedMethod, BindingFlags.Public | BindingFlags.Instance);

			return moveNextMethod;
		}
	}
}