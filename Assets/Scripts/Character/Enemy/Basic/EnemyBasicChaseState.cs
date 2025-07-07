using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicChaseState : EnemyState
{
    public override void EnterState()
    {
        if (behavior != null) behavior.enabled = true;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        if (behavior != null) behavior.enabled = false;
    }

    public override string GetDebugName()
    {
        return "chase";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.WANDER_STATE, () => GetFlattenedVectorToPlayer().magnitude > stateManager.PlayerChaseDistance),
            new StateTransition(EnemyStateManager.ATTACK_STATE, AttackStateCondition),
        };
    }

    private bool AttackStateCondition()
    {
        bool closeEnough = GetFlattenedVectorToPlayer().magnitude < stateManager.AttackProximity;
        bool aimCondition = stateManager.RequireAimForAttack ? Utils.CalculateInFrontFactor(stateManager.OwnTransform, stateManager.PlayerObject.transform, true) > 1 - stateManager.AimTolerance : true;
        return closeEnough && aimCondition;
    }

    private Vector3 GetFlattenedVectorToPlayer()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);
        return flattened;
    }
}
