# Item Choice

Allows the player to choose a powerup of the same tier that they opened from a chest or battle totem. When opening a chest, finishing a battle totem, or killing a boss, a powerup-choosing object will appear instead of a random powerup. Interacting with that object will show a UI to the user to choose a powerup.

### Notes
- I forgot to add a dependency for BepInEx, so you also need BepInExPack_Muck (https://muck.thunderstore.io/package/BepInEx/BepInExPack_Muck/)
- THERES ONLY ASSETS FOR WINDOWS. Other OS may see irregular assets regarding this plugin.
- Go to the github page for a more updated README.md
- Tested in multiplayer to some extent and tested with other mods that add powerups to some extent.
- Possible ideas to be implemented.
- I realized that I called the mod "ItemChoice" instead of "PowerupChoice". That is my mistake...
- I'm not very experienced in Unity.

### Known Issues
- When in multiplayer, if other players are choosing a powerup, and another player chooses a powerup while the other player have the powerup-choosing UI open, the other players will not be able to choose a powerup (as of 1.0.1+, this may or may not be fixed).

### Changelogs
1.1.0:
- Disabled scrolling hotbar when powerup-choosing UI is opened
- Hopefully fixed dropping issue with PowerupDrop (https://muck.thunderstore.io/package/SuperGamerTron/PowerupDrop/).
- Updated the UI a little bit so that it fits more with the Muck theme.
- Added a config to enable or disable if only the powerup-choosing object drops from chests, totems, or bosses (May have issues, but should work for the most part).

1.0.2:
- Fixed an issue where if the player were to time interacting with the powerup-choosing object, they could open the UI again and rechoose a powerup.

1.0.1:
- Tried to fix an issue with not being able to select a powerup when there are multiple powerup-choosing objects opened (singleplayer).
