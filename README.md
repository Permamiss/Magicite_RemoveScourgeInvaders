So you're going through each district, _definitely_ not losing track of time from duplicating items with your friends, when suddenly the Scourge invades! You make a mad dash for the end of the district, but you were too far away and they were too quick. Dang it, there goes a good, fun run. Why do some games even punish you for taking your time?

My friends and I were tired of this happening, so that's why I made this - the first BepInEx plugin for Magicite!

## What does this plugin do?
This plugin prevents the Scourge invaders from spawning. Normally, after a randomized delay of 5 minutes to 5 minutes 50 seconds, they spawn in each district (except for towns and the final district with the boss). This plugin not only prevents them from spawning, but also eliminates the spawn message and stops the music from changing. After using this plugin for a while, you'll be like, "Scourge invaders? What are those?"
<hr>

# Easy Setup Instructions
Use a mod manager like [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or [Thunderstore App](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager) to install the plugin from [thunderstore.io](https://thunderstore.io/c/magicite/p/Permamiss/Remove_Scourge_Invaders/)

# Manual Setup Instructions
## What you need
* __Plugin Loader__: [BepInEx v5.4.23.2](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)
    * 32-bit (x86) version for your OS
* __Plugin__: [RemoveScourgeInvaders.dll](https://github.com/Permamiss/Magicite-RemoveScourgeInvaders/releases)
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
