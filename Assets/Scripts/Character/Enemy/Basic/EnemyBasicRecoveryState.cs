using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//recovery time after an attack
public class EnemyBasicRecoveryState : EnemyState
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
        return "recovery";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.WANDER_STATE, () => stateManager.TimeInState > stateManager.AttackRecoveryDuration)
        };
    }
}
