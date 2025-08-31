using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttack : EnemyBasicState
{
    public override void EnterState()
    {
        stateManager.Attack();
    }

    public override void ExitState()
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
            new StateTransition(EnemyBasicStateManager.PAUSE_STATE, () => stateManager.TimeInState > stateManager.BehaviorData.AttackDuration),
            new StateTransition(EnemyBasicStateManager.DEATH_STATE, () => stateManager.HealthHandler.CurHealth <= 0),
        };
    }

    public override void UpdateState()
    {

    }
}
