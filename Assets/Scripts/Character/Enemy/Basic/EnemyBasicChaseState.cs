using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicChaseState : EnemyState
{
    private float playerChaseDistance;
    private float chaseSpeed;
    private float attackProximity;

    public override void EnterState()
    {
        Debug.Log("Entering chase");
        character.maxWalkSpeed = chaseSpeed;
    }

    public override void UpdateState()
    {
        Vector3 vecToPlayer = playerObj.transform.position - ownTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);

        if (flattened.magnitude > playerChaseDistance)
        {
            stateManager.SwitchState("Wander");
            return;
        } else if (flattened.magnitude < attackProximity)
        {
            stateManager.SwitchState("Attack");
            return;
        }

        character.SetMovementDirection(flattened.normalized);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting chase");
    }

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, float playerChaseDistance, float chaseSpeed, float attackProximity)
    {
        Initialize(stateManager, character, playerObj, ownTransform);
        this.playerChaseDistance = playerChaseDistance;
        this.chaseSpeed = chaseSpeed;
        this.attackProximity = attackProximity;
    }
}
