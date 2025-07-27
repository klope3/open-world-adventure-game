using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : PlayerState
{
    private float initialMoveSpeed;

    public override void EnterState()
    {
        initialMoveSpeed = stateManager.Character.maxWalkSpeed;

        Vector2 inputVec = InputActionsProvider.GetPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(inputVec);
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = initialMoveSpeed;
        InputActionsProvider.UnlockPrimaryAxis();
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.RollDeceleration * stateManager.TimeInState + stateManager.RollSpeed; //deceleration while rolling
    }

    public override string GetDebugName()
    {
        return "roll";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.TimeInState > stateManager.RollDuration),
        };
    }
}
