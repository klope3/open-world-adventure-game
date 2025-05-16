using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class DialogueState : PlayerState
{
    private DialogueManager dialogueManager;
    private InputActionsEvents inputActionsEvents;

    public override void EnterState()
    {
        stateManager.CharacterAdapter.canMove = false;
    }

    public override void ExitState()
    {
        stateManager.CharacterAdapter.canMove = true;
    }

    public override void UpdateState()
    {

    }

    public override void PostInitialize()
    {
        InputActionsProvider.OnInteractButtonStarted += InteractButton_started;

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

    private void InteractButton_started()
    {
        if (stateManager.IsInState(this)) dialogueManager.TryAdvanceDialogue();
    }

    public override string GetDebugName()
    {
        return "dialogue";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[] { };
    }
}
