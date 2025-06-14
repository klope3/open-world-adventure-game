using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicHurtState : EnemyState
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
        return "hurt";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.PAUSE_STATE, () => stateManager.TimeInState >= stateManager.HurtRecoveryTime),
        };
    }
}
