using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basically just an idle state
public class EnemyBasicPauseState : EnemyState
{
    public override void EnterState()
    {
        if (behavior != null) behavior.enabled = true;
    }

    public override void ExitState()
    {
        if (behavior != null) behavior.enabled = false;
    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "pause";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.CHASE_STATE, () => stateManager.ShouldChasePlayer()),
            new StateTransition(EnemyStateManager.WANDER_STATE, () => stateManager.TimeInState > stateManager.WanderPauseTime),
        };
    }
}
