using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LandingState : PlayerState
{
    //private float duration;

    //public System.Action OnEnter;

    public override void EnterState()
    {
        //InputActionsProvider.OnAButtonStarted += Jump_started;
        //InputActionsProvider.OnBButtonStarted += Attack_started;
        //InputActionsProvider.OnInteractButtonStarted += InteractButton_started;
        //InputActionsProvider.OnZTargetStarted += ZTarget_started;
        //
        //OnEnter?.Invoke();
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "landing";
    }

    //public void Initialize(PlayerStateManager stateManager, ECM2.Character character, ECM2CharacterAdapter characterAdapter, float duration)
    //{
    //    Initialize(stateManager, character, characterAdapter);
    //    this.duration = duration;
    //}

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
        //if (stateManager.TimeInState > duration || InputActionsProvider.GetPrimaryAxis().magnitude > 0) stateManager.ToDefaultState();
    }

    public override StateTransition[] GetTransitions()
    {
        StateTransition[] defaultTransitions = stateManager.GetDefaultTransitions();
        StateTransition[] additionalTransitions = new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, () => stateManager.TimeInState > stateManager.LandingDuration || InputActionsProvider.GetPrimaryAxis().magnitude > 0),
        };
        StateTransition[] allTransitions = defaultTransitions.Concat(additionalTransitions).ToArray();
        return allTransitions;
    }
}
