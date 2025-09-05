using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicChase : EnemyBasicState
{
    private float prevWalkSpeed;

    public override void EnterState()
    {
        prevWalkSpeed = stateManager.Character.maxWalkSpeed;
        stateManager.Character.maxWalkSpeed = stateManager.BehaviorData.ChaseSpeed;
        stateManager.ChaseGameObject.target = stateManager.TargetHandler.Target;
        stateManager.ChaseGameObject.enabled = true;
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = prevWalkSpeed;
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
            new StateTransition(EnemyBasicStateManager.PAUSE_STATE, () => stateManager.TargetHandler.Target == null),
            new StateTransition(EnemyBasicStateManager.ATTACK_STATE, ToAttack),
            new StateTransition(EnemyBasicStateManager.DEATH_STATE, () => stateManager.HealthHandler.CurHealth <= 0),
        };
    }

    private bool ToAttack()
    {
        bool hasTarget = stateManager.TargetHandler.Target != null;
        if (!hasTarget) return false;

        bool closeEnough = Vector3.Distance(stateManager.transform.position, stateManager.TargetHandler.Target.transform.position) < stateManager.BehaviorData.AttackDistance;
        bool facing = Utils.CalculateInFrontFactor(stateManager.transform, stateManager.TargetHandler.Target.transform, true) > 0.999f;
        return closeEnough && facing;
    }

    public override void UpdateState()
    {

    }
}
