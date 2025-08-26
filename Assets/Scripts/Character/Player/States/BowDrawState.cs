using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDrawState : PlayerState
{
    private PlayerDefaultMovementModule.MovementType initialMovementType;

    public override void EnterState()
    {
        //stateManager.DefaultMovementModule.canMove = false;
        initialMovementType = stateManager.DefaultMovementModule.CurrentMovementType;
        stateManager.DefaultMovementModule.SetMovementType(PlayerDefaultMovementModule.MovementType.Strafe);
        stateManager.ArrowLauncher.SetTriggerState(false);
        stateManager.Character.maxWalkSpeed = stateManager.PlayerControlDataSO.BowMoveSpeed;

        InputActionsProvider.OnBButtonCanceled += InputActionsProvider_OnBButtonCanceled;
    }

    public override void ExitState()
    {
        stateManager.DefaultMovementModule.SetMovementType(initialMovementType);
        stateManager.DefaultMovementModule.canMove = true;
        stateManager.Character.maxWalkSpeed = stateManager.PlayerControlDataSO.DefaultMoveSpeed;

        InputActionsProvider.OnBButtonCanceled -= InputActionsProvider_OnBButtonCanceled;
    }

    private void InputActionsProvider_OnBButtonCanceled()
    {
        stateManager.trigger = PlayerStateManager.MOVING_STATE;
    }

    public override string GetDebugName()
    {
        return "bow draw";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.trigger == PlayerStateManager.MOVING_STATE),
            new StateTransition(PlayerStateManager.BOW_HOLD_STATE, () => stateManager.TimeInState >= stateManager.PlayerControlDataSO.BowDrawDuration),
        };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
