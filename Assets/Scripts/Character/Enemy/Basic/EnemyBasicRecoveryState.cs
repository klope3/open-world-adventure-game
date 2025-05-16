using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//recovery time after an attack
public class EnemyBasicRecoveryState : EnemyState
{
    public override void EnterState()
    {
        stateManager.Character.SetMovementDirection(Vector3.zero);
    }

    public override void ExitState()
    {
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
            new StateTransition(EnemyStateManager.WANDER_STATE, () => stateManager.TimeInState > stateManager.AttackRecoveryDuration)
        };
    }
}
