using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : PlayerState
{
    private float initialMoveSpeed;
    private float initialAcceleration;

    public System.Action OnEnter;

    public override void EnterState()
    {
        initialMoveSpeed = stateManager.Character.maxWalkSpeed;
        initialAcceleration = stateManager.Character.maxAcceleration;

        Vector2 inputVec = InputActionsProvider.GetPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(inputVec);

        stateManager.Character.maxAcceleration = 1000; //max mobility during dodge

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = initialMoveSpeed;
        stateManager.Character.maxAcceleration = initialAcceleration;
        InputActionsProvider.UnlockPrimaryAxis();
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.DodgeDeceleration * stateManager.TimeInState + stateManager.DodgeSpeed; //deceleration while rolling
    }

    public override string GetDebugName()
    {
        return "dodge";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.TimeInState >= stateManager.DodgeDuration),
        };
    }
}
