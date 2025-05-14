using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class RollState : PlayerState
{
    //private float speed;
    //private float duration;
    private float initialMoveSpeed;
    //private float deceleration;

    //public System.Action OnEnter;
    //public System.Action OnExit;

    public override void EnterState()
    {
        initialMoveSpeed = stateManager.Character.maxWalkSpeed;

        Vector2 inputVec = InputActionsProvider.GetPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(inputVec);

        //OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = initialMoveSpeed;
        InputActionsProvider.UnlockPrimaryAxis();

        //OnExit?.Invoke();
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        //if (stateManager.TimeInState >= duration)
        //{
        //    stateManager.SwitchState(PlayerStateManager.DEFAULT_STATE);
        //    return;
        //}
        stateManager.Character.maxWalkSpeed = stateManager.RollDeceleration * stateManager.TimeInState + stateManager.RollSpeed; //deceleration while rolling
    }

    //public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, float speed, float duration, float deceleration)
    //{
    //    Initialize(stateManager, character, characterAdapter);
    //    this.speed = speed;
    //    this.duration = duration;
    //    this.deceleration = deceleration;
    //}

    public override string GetDebugName()
    {
        return "roll";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, () => stateManager.TimeInState > stateManager.RollDuration),
        };
    }
}
