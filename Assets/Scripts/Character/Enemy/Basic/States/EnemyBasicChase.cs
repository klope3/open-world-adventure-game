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
        return stateManager.Target != null && Vector3.Distance(stateManager.transform.position, stateManager.Target.transform.position) < stateManager.BehaviorData.AttackDistance;
    }

    public override void UpdateState()
    {

    }
}
