using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicWanderState : EnemyState
{
    public override void EnterState()
    {
        if (behavior != null) behavior.enabled = true;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        if (behavior != null) behavior.enabled = false;
    }

    public override string GetDebugName()
    {
        return "wander";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.CHASE_STATE, () => stateManager.ShouldChasePlayer()),
            new StateTransition(EnemyStateManager.PAUSE_STATE, () => stateManager.TimeInState >= stateManager.WanderMoveTime),
        };
    }
}
