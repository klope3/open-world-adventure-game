using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//the old dialogue system had dialogue initiators actually "doing" the dialogue initiating.
//it would be better for simple dialogue objects like signs to be passive data holders, like chests.
//NPCs may have different needs.
public abstract class DialogueInitiator : MonoBehaviour, IInteractable
{
    protected DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        if (!dialogueManager) Debug.LogError($"DialogueInitiator '{name}' failed to find the dialogue manager!");
    }

    public abstract DialogueNodeSO ChooseStartingNode();

    public void DoInteraction(PlayerInteractionHandler interactionHandler)
    {
        dialogueManager.InitiateDialogue(this);
    }

    public string GetInteractionName()
    {
        return "Speak";
    }
}
