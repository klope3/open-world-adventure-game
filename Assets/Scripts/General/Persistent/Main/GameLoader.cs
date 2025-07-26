using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//hooks into the save slot menu (which should be game-agnostic) and then runs logic specific to this game to load everything and start playing 
//ACCOUNT FOR EDGE CASES WHERE POSSIBLE
//corrupted file (e.g. a string has become a float)
//directory does not exist
//file does not exist
public class GameLoader : MonoBehaviour, ISaveSlotLabelProvider
{
    [SerializeField] private DefaultGameDataSO defaultGameData;
    [SerializeField] private SelectedSaveSlotMenu selectedSaveSlotMenu;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string playGameSceneName; //this will need to be dynamic eventually since the player might have saved in any scene

    private void Awake()
    {
        selectedSaveSlotMenu.OnSuccessfulPlayGameRequest += SelectedSaveSlotMenu_OnSuccessfulPlayGameRequest;
    }

    private void SelectedSaveSlotMenu_OnSuccessfulPlayGameRequest(string saveFile, string slotName)
    {
        if (saveFile != "")
        {
            try
            {
                SaveData loadedSaveData = ES3.Load<SaveData>("SaveData", Path.GetFileName(saveFile));
                PersistentGameData.SetSaveData(loadedSaveData);
                PersistentGameData.activeSaveFile = saveFile;
                sceneTransition.TransitionToScene(playGameSceneName, 0);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        } else
        {
            SaveData newSaveData = new SaveData();
            PlayerData newPlayerData = defaultGameData.GetPlayerData();
            newPlayerData.name = slotName;
            newSaveData.SetPlayerData(newPlayerData);
            PersistentGameData.SetSaveData(newSaveData);
            PersistentGameData.activeSaveFile = GameSaver.CreateSaveFileName(slotName);
            sceneTransition.TransitionToScene(playGameSceneName, 0);
        }

    }

    public string GetSaveSlotLabel(string file)
    {
        try
        {
            SaveData loadedSaveData = ES3.Load<SaveData>("SaveData", Path.GetFileName(file));
            return loadedSaveData.PlayerData.name;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return "BAD_DATA";
        }
    }
}
