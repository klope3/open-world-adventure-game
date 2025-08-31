using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicPause : EnemyBasicState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
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
            new StateTransition(EnemyBasicStateManager.WANDER_STATE, () => stateManager.TimeInState > stateManager.BehaviorData.PauseTime),
            new StateTransition(EnemyBasicStateManager.CHASE_STATE, () => stateManager.ShouldChaseTarget()),
            new StateTransition(EnemyBasicStateManager.DEATH_STATE, () => stateManager.HealthHandler.CurHealth <= 0),
        };
    }

    public override void UpdateState()
    {

    }
}
