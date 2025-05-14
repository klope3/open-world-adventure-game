using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicChaseState : EnemyState
{
    //private float playerChaseDistance;
    //private float chaseSpeed;
    //private float attackProximity;

    public override void EnterState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.ChaseSpeed;
    }

    public override void UpdateState()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);

        //if (flattened.magnitude > playerChaseDistance)
        //{
        //    stateManager.SwitchState("Wander");
        //    return;
        //} else if (flattened.magnitude < attackProximity)
        //{
        //    stateManager.SwitchState("Attack");
        //    return;
        //}

        stateManager.Character.SetMovementDirection(flattened.normalized);
    }

    public override void ExitState()
    {
    }

    //public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, float playerChaseDistance, float chaseSpeed, float attackProximity)
    //{
    //    Initialize(stateManager, character, playerObj, ownTransform);
    //    this.playerChaseDistance = playerChaseDistance;
    //    this.chaseSpeed = chaseSpeed;
    //    this.attackProximity = attackProximity;
    //}

    public override string GetDebugName()
    {
        return "chase";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.WANDER_STATE, ToWander),
            new StateTransition(EnemyStateManager.ATTACK_STATE, ToAttack),
        };
    }

    private bool ToWander()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);
        return flattened.magnitude > stateManager.PlayerChaseDistance;
    }

    private bool ToAttack()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);
        return flattened.magnitude < stateManager.AttackProximity;
    }
}
