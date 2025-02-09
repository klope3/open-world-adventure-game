using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class Attack2State : PlayerState
{
    private float timeInState;
    private float chainDelay;

    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        Debug.Log("Entering attack2");
        characterAdapter.canMove = false;
        InputActionsProvider.OnBButtonStarted += Attack_started;

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
        Debug.Log("Exiting attack2");
        InputActionsProvider.OnBButtonStarted -= Attack_started;
        OnExit?.Invoke();
    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, float timeInState, float chainDelay)
    {
        Initialize(stateManager, character, characterAdapter);
        this.timeInState = timeInState;
        this.chainDelay = chainDelay;
    }

    public override void PostInitialize()
    {
    }

    private void Attack_started()
    {
        if (stateManager.TimeInState > chainDelay) stateManager.SwitchState("Attack");
    }
}
