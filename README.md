# Item Choice

Allows the player to choose a powerup of the same tier that they opened from a chest or battle totem. 

### Notes

- Tested in multiplayer to some extent and NOT tested with other mods that add powerups.
- Possible ideas to be implemented.
- I'm not very experienced in Unity.

### Known Issues
- When in multiplayer, if other players are choosing a powerup, and another player chooses a powerup while the other player have the powerup-choosing ui open, the other players will not be able to choose a powerup.
- With PowerupDrop (https://muck.thunderstore.io/package/SuperGamerTron/PowerupDrop/), dropped powerups will be dropped as if a chest was opened.
- When choosing powerup, if the player were to time their interaction button as soon as they chose a powerup, the powerup-choosing ui would be opened again and would be able to change the powerup again, assuming the powerup has not been picked up yet.

### Changelogs

1.0.1:
- Tried to fix an issue with not being able to select a powerup when there are multiple powerup-choosing objects opened (singleplayer).
