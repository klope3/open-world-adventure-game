using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A non-dynamic dialogue initiator. It always initiates the same dialogue tree. Useful for things like signs that will always say the same thing.
public class DialogueInitiatorStatic : DialogueInitiator, IInteractable
{
    [SerializeField] private DialogueNodeSO initialNode;

    private void Awake()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public override DialogueNodeSO ChooseStartingNode()
    {
        return initialNode;
    }

    public void DoInteraction()
    {
        dialogueManager.InitiateDialogue(this);
    }
}
