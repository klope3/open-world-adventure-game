using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class AttackState : PlayerState
{
    private float timeInState;
    private readonly float DURATION = 0.6f;
    //private float chainDelay;

    //public System.Action OnEnter;
    //public System.Action OnExit;

    public override void EnterState()
    {
        stateManager.CharacterAdapter.canMove = false;
        stateManager.IncrementRecentStandardAttacks();

        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        if (inputVec.magnitude > 0.05f)
        {
            inputVec = new Vector3(inputVec.x, 0, inputVec.y);
            inputVec = inputVec.relativeTo(stateManager.Character.cameraTransform);
            stateManager.Character.SetRotation(Quaternion.LookRotation(inputVec)); //ensure that player faces direction of input instantly; prevents getting stuck in an attack animation while still turning around
        }
        //InputActionsProvider.OnBButtonStarted += Attack_started;

        //OnEnter?.Invoke();
    }
    
    public override void UpdateState()
    {
        if (stateManager.TimeInState > timeInState)
        {
            //stateManager.SwitchState(PlayerStateManager.DEFAULT_STATE);
        }
    }
    
    public override void ExitState()
    {
        stateManager.attackInput = false;
        //InputActionsProvider.OnBButtonStarted -= Attack_started;
        stateManager.CharacterAdapter.canMove = true;
        //OnExit?.Invoke();
    }

    //public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, float timeInState)
    //{
    //    Initialize(stateManager, character, characterAdapter);
    //    this.timeInState = timeInState;
    //    //this.chainDelay = chainDelay;
    //}

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "attack";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, ToDefaultState),
        };
    }

    private bool ToDefaultState()
    {
        return stateManager.TimeInState > DURATION;
    }

    //private void Attack_started()
    //{
    //    if (stateManager.TimeInState > chainDelay) stateManager.SwitchState("Attack2");
    //}
}
