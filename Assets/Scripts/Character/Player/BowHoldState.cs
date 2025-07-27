using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHoldState : PlayerState
{
    private PlayerDefaultMovementModule.MovementType initialMovementType;

    public override void EnterState()
    {
        stateManager.DefaultMovementModule.canMove = false;
        initialMovementType = stateManager.DefaultMovementModule.CurrentMovementType;
        stateManager.DefaultMovementModule.SetMovementType(PlayerDefaultMovementModule.MovementType.Strafe);

        InputActionsProvider.OnBButtonCanceled += InputActionsProvider_OnBButtonCanceled;
    }

    public override void ExitState()
    {
        stateManager.DefaultMovementModule.SetMovementType(initialMovementType);
        stateManager.DefaultMovementModule.canMove = true;

        InputActionsProvider.OnBButtonCanceled -= InputActionsProvider_OnBButtonCanceled;
    }

    private void InputActionsProvider_OnBButtonCanceled()
    {
        stateManager.ArrowLauncher.SetTriggerState(true);
        stateManager.trigger = PlayerStateManager.MOVING_STATE;
    }

    public override string GetDebugName()
    {
        return "bow hold";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.trigger == PlayerStateManager.MOVING_STATE),
        };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
