using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ACCOUNT FOR EDGE CASES WHERE POSSIBLE
//no storage space left
//directory does not exist
//empty file name
//file is in use
public static class GameSaver
{
    public static bool TrySave()
    {
        PersistentGameData.SaveData.version = Application.version;
        try
        {
            ES3.Save("SaveData", PersistentGameData.SaveData, PersistentGameData.activeSaveFile);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return false;
            //maybe for some error cases we could show the player some message? 
            //this only makes sense for cases they might have control over, e.g. file is in use, no storage space, etc.
        }
    }

    public static string CreateSaveFileName(string slotName)
    {
        System.Guid guid = System.Guid.NewGuid();
        return $"{slotName}-{guid}.es3";
    }
}
