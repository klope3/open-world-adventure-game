using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicHurtState : EnemyState
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
