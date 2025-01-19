using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class DialogueState : PlayerState
{
    private DialogueManager dialogueManager;
    private DialogueBox dialogueBox;
    private InputActionsEvents inputActionsEvents;

    public override void EnterState()
    {
        characterAdapter.canMove = false;
    }

    public override void ExitState()
    {
        characterAdapter.canMove = true;
    }

    public override void UpdateState()
    {

    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, DialogueManager dialogueManager, DialogueBox dialogueBox, InputActionsEvents inputActionsEvents)
    {
        Initialize(stateManager, character, characterAdapter);
        this.dialogueManager = dialogueManager;
        this.dialogueBox = dialogueBox;
        this.inputActionsEvents = inputActionsEvents;
    }

    public override void PostInitialize()
    {
        InputActionsProvider.InputActions.Player.InteractButton.started += InteractButton_started;

        inputActionsEvents.OnPrimaryDirectionalAxisStartedUp += InputActionsEvents_OnPrimaryDirectionalAxisStartedUp;
        inputActionsEvents.OnPrimaryDirectionalAxisStartedDown += InputActionsEvents_OnPrimaryDirectionalAxisStartedDown;
    }

    private void InputActionsEvents_OnPrimaryDirectionalAxisStartedDown()
    {
        if (stateManager.IsInState(this)) dialogueManager.IncrementChoiceIndex(1);
    }

    private void InputActionsEvents_OnPrimaryDirectionalAxisStartedUp()
    {
        if (stateManager.IsInState(this)) dialogueManager.IncrementChoiceIndex(-1);
    }

    private void InteractButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (stateManager.IsInState(this)) dialogueManager.TryAdvanceDialogue();
    }
}
