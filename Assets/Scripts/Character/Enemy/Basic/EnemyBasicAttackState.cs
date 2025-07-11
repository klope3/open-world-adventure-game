using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackState : EnemyState
{
    private bool hurt;

    public override void EnterState()
    {
        if (behavior != null) behavior.enabled = true;

        stateManager.OwnHealth.OnDamaged += HealthHandler_OnDamaged;
    }

    private void HealthHandler_OnDamaged(Vector3 position)
    {
        hurt = true;
    }

    public override void ExitState()
    {
        stateManager.OwnHealth.OnDamaged -= HealthHandler_OnDamaged;
        hurt = false;
        if (behavior != null) behavior.enabled = false;
    }

    public override void UpdateState() { }

    public override string GetDebugName()
    {
        return "attack";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.RECOVERY_STATE, () => hurt || stateManager.TimeInState > stateManager.AttackDuration),
            new StateTransition(EnemyStateManager.WANDER_STATE, () => stateManager.PlayerObject.GetComponent<HealthHandler>().CurHealth <= 0),
        };
    }
}
