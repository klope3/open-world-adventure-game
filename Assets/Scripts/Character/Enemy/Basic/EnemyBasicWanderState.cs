using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicWanderState : EnemyState
{
    private float playerChaseDistance;
    private float moveSpeed;
    private float timer;
    private float timerMax;
    private float pauseTimerMax;
    private bool moving;

    public override void EnterState()
    {
        Debug.Log("Entering wander");
        character.maxWalkSpeed = moveSpeed;
        character.SetMovementDirection(PickRandomDirection());
        timer = 0;
        moving = true;
    }

    public override void UpdateState()
    {
        Vector3 vecToPlayer = playerObj.transform.position - ownTransform.position;
        if (vecToPlayer.magnitude < playerChaseDistance)
        {
            stateManager.SwitchState("Chase");
            return;
        }

        timer += Time.deltaTime;

        if (timer > timerMax && moving)
        {
            character.SetMovementDirection(Vector3.zero);
            moving = false;
            timer = 0;
        } else if (timer > pauseTimerMax && !moving)
        {
            character.SetMovementDirection(PickRandomDirection());
            moving = true;
            timer = 0;
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting wander");
    }

    private Vector3 PickRandomDirection()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        return new Vector3(rand.x, 0, rand.y);
    }

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, float wanderMoveSpeed, float wanderMoveTime, float wanderPauseTime, float playerChaseDistance)
    {
        Initialize(stateManager, character, playerObj, ownTransform);
        timerMax = wanderMoveTime;
        pauseTimerMax = wanderPauseTime;
        moveSpeed = wanderMoveSpeed;
        this.playerChaseDistance = playerChaseDistance;
    }
}
