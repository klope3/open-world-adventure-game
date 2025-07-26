using System.Collections;
using System.Collections.Generic;

//All data that needs to persist across scenes. Some also persists across play sessions.
public static class PersistentGameData
{
    private static SaveData saveData;
    public static string activeSaveFile = "";
    public static int sceneTransitionIndex; //spawn point index to use for next scene while transitioning scenes

    public static SaveData SaveData
    {
        get
        {
            if (saveData == null) saveData = new SaveData();
            return saveData;
        }
    }

    public static void SetSaveData(SaveData newSaveData)
    {
        saveData = newSaveData;
    }
}
