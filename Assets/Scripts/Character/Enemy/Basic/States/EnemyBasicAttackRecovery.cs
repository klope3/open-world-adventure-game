using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackRecovery : EnemyBasicState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "attack recovery";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyBasicStateManager.WANDER_STATE, () => stateManager.TimeInState > stateManager.BehaviorData.AttackRecoveryTime),
        };
    }

    public override void UpdateState()
    {

    }
}
