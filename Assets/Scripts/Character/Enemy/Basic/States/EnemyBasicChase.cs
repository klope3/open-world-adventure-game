using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicChase : EnemyBasicState
{
    public override void EnterState()
    {
        stateManager.ChaseGameObject.target = stateManager.Target;
        stateManager.ChaseGameObject.enabled = true;
    }

    public override void ExitState()
    {
        stateManager.ChaseGameObject.enabled = false;
    }

    public override string GetDebugName()
    {
        return "chase";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyBasicStateManager.PAUSE_STATE, () => stateManager.Target == null),
            new StateTransition(EnemyBasicStateManager.ATTACK_STATE, ToAttack),
            new StateTransition(EnemyBasicStateManager.DEATH_STATE, () => stateManager.HealthHandler.CurHealth <= 0),
        };
    }

    private bool ToAttack()
    {
        bool hasTarget = stateManager.Target != null;
        if (!hasTarget) return false;

        bool closeEnough = Vector3.Distance(stateManager.transform.position, stateManager.Target.transform.position) < stateManager.BehaviorData.AttackDistance;
        bool facing = Utils.CalculateInFrontFactor(stateManager.transform, stateManager.Target.transform, true) > 0.999f;
        return closeEnough && facing;
    }

    public override void UpdateState()
    {

    }
}
