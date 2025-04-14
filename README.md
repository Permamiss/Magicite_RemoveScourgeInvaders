# Setup Instructions
## What you need
* **Mod Loader**: [BepInEx v5.4.23.2](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)
    * 32-bit (x86) version for your OS
* **Plugin**: [RemoveScourgeInvaders.dll](https://github.com/Permamiss/Magicite-RemoveScourgeInvaders/releases)
    * The latest release version

## Installation
1. Extract `BepInEx_[...]_x86_[...].zip` to the Magicite game folder (_hereafter referred to as `[game_folder]`_)
   > _Default Magicite game folder location:_<br>
   > `C:\Program Files (x86)\Steam\steamapps\common\Magicite`
2. __Modify the BepInEx cfg file so that it can boot without crashing__
   1. Navigate to `[game_folder]\BepInEx\config`, right-click `BepInEx.cfg` and edit it
   2. Scroll all the way to the bottom of the file until you are in the `[Preloader.Entrypoint]` section
   3. Change the `Type = Application` line to `Type = MonoBehaviour`
      * _(Optional) If you would like to see a debug console, then go to the `[Logging.Console]` section towards the top of the file, change `Enabled = false` to `Enabled = true`_
   4. Save your changes, then close the file
3. __Load the plugin__
    1. Move the downloaded plugin from your `Downloads` folder to `[game_folder]\BepInEx\plugins`
       > If there is not a `plugins` folder here, then create one yourself (right-click > New > Folder)
4. __Play your newly modded game!__
