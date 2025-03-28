using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class AttackState : PlayerState
{
    private float timeInState;
    //private float chainDelay;

    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        characterAdapter.canMove = false;
        //InputActionsProvider.OnBButtonStarted += Attack_started;

        OnEnter?.Invoke();
    }
    
    public override void UpdateState()
    {
        if (stateManager.TimeInState > timeInState)
        {
            stateManager.SwitchState("Idle");
        }
    }
    
    public override void ExitState()
    {
        //InputActionsProvider.OnBButtonStarted -= Attack_started;
        OnExit?.Invoke();
    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, float timeInState)
    {
        Initialize(stateManager, character, characterAdapter);
        this.timeInState = timeInState;
        //this.chainDelay = chainDelay;
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "attack";
    }

    //private void Attack_started()
    //{
    //    if (stateManager.TimeInState > chainDelay) stateManager.SwitchState("Attack2");
    //}
}
