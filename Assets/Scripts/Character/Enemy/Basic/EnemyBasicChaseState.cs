using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicChaseState : EnemyState
{
    public override void EnterState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.ChaseSpeed;
    }

    public override void UpdateState()
    {
        stateManager.Character.SetMovementDirection(GetFlattenedVectorToPlayer().normalized);
    }

    public override void ExitState()
    {
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
            new StateTransition(EnemyStateManager.ATTACK_STATE, () => GetFlattenedVectorToPlayer().magnitude < stateManager.AttackProximity),
        };
    }

    private Vector3 GetFlattenedVectorToPlayer()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);
        return flattened;
    }
}
