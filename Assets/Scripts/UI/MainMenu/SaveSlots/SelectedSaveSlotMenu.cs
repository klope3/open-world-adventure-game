using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedSaveSlotMenu : MonoBehaviour
{
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI actionText; //text for the main button that loads the save file or creates a new one
    [SerializeField, Tooltip("The text to display on the 'continue game' button for existing save files.")] private string continueText = "CONTINUE";
    [SerializeField, Tooltip("The text to display on the 'new game' button for new save files.")] private string startText = "START NEW GAME";

    private string file;
    
    public delegate void PlayGameEvent(string saveFile, string slotName);
    public event PlayGameEvent OnSuccessfulPlayGameRequest;

    //for managing an existing save file
    public void EnableWithData(string slotName, string file)
    {
        this.file = file;
        inputField.text = slotName;
        actionText.text = continueText;

        gameObject.SetActive(true);
    }

    //for creating a new save file
    public void EnableEmpty()
    {
        file = "";
        inputField.text = "";
        actionText.text = startText;

        gameObject.SetActive(true);
    }

    public void PlayGameClicked()
    {
        if (inputField.text == "")
        {
            Debug.LogError("Name is required.");
            return;
        }

        OnSuccessfulPlayGameRequest?.Invoke(file, inputField.text);
    }
}
