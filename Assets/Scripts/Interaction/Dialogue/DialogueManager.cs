using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private NonPlayerCharacterManager npcManager;
    [SerializeField] private WeatherManager weatherManager;
    private DialogueNodeSO currentNode;
    private int selectedChoiceIndex;
    public int SelectedChoiceIndex
    {
        get
        {
            return selectedChoiceIndex;
        }
    }

    public void InitiateDialogue(DialogueInitiator initiator)
    {
        currentNode = initiator.ChooseStartingNode();
        while (currentNode.Passthru)
        {
            currentNode = currentNode.ChooseNextNode();
        }

        dialogueBox.Print(currentNode.Text, currentNode.HasChoices());
        //playerStateManager.SwitchState("Dialogue");
        npcManager.SetEnemiesFrozen(true);
        weatherManager.enabled = false;
        selectedChoiceIndex = 0;
    }

    public void TryAdvanceDialogue()
    {
        if (dialogueBox.FinishedPrinting())
        {
            if (currentNode.ChooseNextNode() == null)
            {
                StopDialogue();
            } else
            {
                currentNode = currentNode.ChooseNextNode();
                dialogueBox.Print(currentNode.Text, currentNode.HasChoices());
            }
        }
    }

    private void StopDialogue()
    {
        dialogueBox.Hide();
        //playerStateManager.SwitchState("Idle");
        npcManager.SetEnemiesFrozen(false);
        weatherManager.enabled = true;
    }

    public string[] GetDialogueChoices()
    {
        string[] choices = { };
        if (currentNode.ResponseChoices == null) return choices;
        return currentNode.ResponseChoices;
    }

    public bool AnyFurtherNodes()
    {
        return currentNode.ChooseNextNode() != null;
    }

    public void IncrementChoiceIndex(int increment)
    {
        if (!dialogueBox.FinishedPrinting()) return;

        string[] choices = currentNode.ResponseChoices;
        if (choices == null || choices.Length < 2) return;

        selectedChoiceIndex += increment;
        if (selectedChoiceIndex < 0) selectedChoiceIndex = choices.Length - 1;
        if (selectedChoiceIndex > choices.Length - 1) selectedChoiceIndex = 0;

        dialogueBox.UpdateChoiceIndicators();
    }
}
