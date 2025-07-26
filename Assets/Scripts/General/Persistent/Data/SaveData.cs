using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //this attribute is necessary because ES3 doesn't serialize private fields by default
    [ES3Serializable] private PlayerData playerData;
    [ES3Serializable] private WorldData worldData;
    public string version;

    public PlayerData PlayerData
    {
        get
        {
            if (playerData == null) playerData = new PlayerData();
            return playerData;
        }
    }

    public WorldData WorldData
    {
        get
        {
            if (worldData == null) worldData = new WorldData();
            return worldData;
        }
    }
    public void SetPlayerData(PlayerData newPlayerData)
    {
        playerData = newPlayerData;
    }
}
