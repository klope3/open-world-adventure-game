using System.Collections;
using System.Collections.Generic;

//All data that needs to persist across scenes. Will eventually also be used for saving/loading to persist across play sessions.
//This will eventually contain instances of multiple classes (e.g. PlayerData, DungeonData, etc.)
//Otherwise it will probably get very large and unmanageable.
public static class PersistentGameData
{
    public static int sceneTransitionIndex; //spawn point index to use for next scene while transitioning scenes
    public static int playerHealth = 7; //this kind of initial constant should only be used when the player starts a brand new game; will need to be handled differently in future
    public static int playerMoney;
}
