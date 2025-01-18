using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private WeatherManager weatherManager;
    private DialogueNodeSO currentNode;

    public void InitiateDialogue(DialogueInitiator initiator)
    {
        currentNode = initiator.ChooseStartingNode();
        dialogueBox.Print(currentNode.Text);
        playerStateManager.SwitchState("Dialogue");
        weatherManager.enabled = false;
    }

    public void TryAdvanceDialogue()
    {
        if (dialogueBox.FinishedPrinting())
        {
            if (currentNode.NextNode == null)
            {
                StopDialogue();
            } else
            {
                currentNode = currentNode.NextNode;
                dialogueBox.Print(currentNode.Text);
            }
        }
    }

    private void StopDialogue()
    {
        dialogueBox.Hide();
        playerStateManager.SwitchState("Idle");
        weatherManager.enabled = true;
    }

    public bool AnyFurtherNodes()
    {
        return currentNode.NextNode != null;
    }
}
