using System.Reflection.Emit;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;

namespace RemoveScourgeInvaders
{
	[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
	[BepInProcess("Magicite.exe")]
	public class RemoveInvader : BaseUnityPlugin
	{
		public void Awake()
		{
			Harmony.CreateAndPatchAll(typeof(RemoveInvader), "RemoveInvader");
		}

		[HarmonyPatch(typeof(GameScript), "Invader")]
		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> RemoveInvaderTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			CodeMatcher codeMatcher = new CodeMatcher(instructions);

			codeMatcher.MatchForward
			(
				false,
				new CodeMatch(OpCodes.Ldarg_0)
			).RemoveInstruction().Insert(new CodeInstruction(OpCodes.Ldnull));

			return codeMatcher.Instructions();
		}
	}
}