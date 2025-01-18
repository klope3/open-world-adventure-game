using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class DialogueState : PlayerState
{
    private DialogueManager dialogueManager;

    public override void EnterState()
    {
        Debug.Log("Entering dialogue state");
        characterAdapter.canMove = false;
    }

    public override void ExitState()
    {
        characterAdapter.canMove = true;
    }

    public override void UpdateState()
    {

    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, DialogueManager dialogueManager)
    {
        Initialize(stateManager, character, characterAdapter);
        this.dialogueManager = dialogueManager;
    }

    protected override void PostInitialize()
    {
        InputActionsProvider.InputActions.Player.InteractButton.started += InteractButton_started;
    }

    private void InteractButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (stateManager.IsInState(this)) dialogueManager.TryAdvanceDialogue();
    }
}
