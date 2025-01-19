using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class DialogueInitiator : MonoBehaviour, IInteractable
{
    protected DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        if (!dialogueManager) Debug.LogError($"DialogueInitiator '{name}' failed to find the dialogue manager!");
    }

    public abstract DialogueNodeSO ChooseStartingNode();

    public void DoInteraction()
    {
        dialogueManager.InitiateDialogue(this);
    }
}
