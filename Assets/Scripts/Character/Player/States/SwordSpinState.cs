using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpinState : PlayerState
{
    public override void EnterState()
    {
        Vector2 snappedInputVec = InputActionsProvider.GetSnappedPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(snappedInputVec);
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.cachedPlayerSpeed;
        stateManager.Character.maxAcceleration = stateManager.cachedPlayerAcceleration;
        InputActionsProvider.UnlockPrimaryAxis();
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.CalculateDodgeMoveSpeed(stateManager.TimeInState);
    }

    public override string GetDebugName()
    {
        return "sword spin";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.TimeInState >= stateManager.PlayerControlDataSO.SwordSpinDuration),
        };
    }
}
