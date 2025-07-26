using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//this should all be game-agnostic
public class SaveSlotMenu : MonoBehaviour
{
    [SerializeField] private SaveSlotButton slotPf;
    [SerializeField] private SelectedSaveSlotMenu selectedSlotMenu;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private MonoBehaviour slotLabelProvider;
    [SerializeField] private string newGameLabel = "New";

    private void OnValidate()
    {
        if (slotLabelProvider != null && slotLabelProvider.GetComponent<ISaveSlotLabelProvider>() == null)
        {
            Debug.LogError("slotLabelProvider must implement ISaveSlotLabelProvider.");
            slotLabelProvider = null;
        }
    }

    private void OnEnable()
    {
        string[] files = GetSaveFiles();
        foreach (string file in files)
        {
            SaveSlotButton newButton = Instantiate(slotPf, slotsParent);
            string label = slotLabelProvider.GetComponent<ISaveSlotLabelProvider>().GetSaveSlotLabel(file);
            newButton.SetText(label);
            newButton.Button.onClick.AddListener(() => OnSlotClick(file));
        }
        SaveSlotButton newSlotButton = Instantiate(slotPf, slotsParent);
        newSlotButton.SetText("New");
        newSlotButton.Button.onClick.AddListener(OnClickNewSlot);
    }

    private void OnDisable()
    {
        foreach (Transform t in slotsParent)
        {
            Destroy(t.gameObject);
        }
    }

    private void OnSlotClick(string file)
    {
        string label = slotLabelProvider.GetComponent<ISaveSlotLabelProvider>().GetSaveSlotLabel(file);
        selectedSlotMenu.EnableWithData(label, file);
    }

    private void OnClickNewSlot()
    {
        selectedSlotMenu.EnableEmpty();
    }

    private string[] GetSaveFiles()
    {
        string saveDir = Application.persistentDataPath;

        if (!Directory.Exists(saveDir))
        {
            Debug.LogError("Save directory does not exist.");
            return new string[] { };
        }

        return Directory.GetFiles(saveDir, "*.es3");
    }
} 
