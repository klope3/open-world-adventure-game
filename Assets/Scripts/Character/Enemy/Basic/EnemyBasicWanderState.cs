using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicWanderState : EnemyState
{
    private float timer;
    private bool moving; //when false, stop moving; this "pause" behavior is separate from the "pause" state

    public override void EnterState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.WanderSpeed;
        stateManager.Character.SetMovementDirection(PickRandomDirection());
        timer = 0;
        moving = true;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;

        if (timer > stateManager.WanderMoveTime && moving)
        {
            stateManager.Character.SetMovementDirection(Vector3.zero);
            moving = false;
            timer = 0;
        } else if (timer > stateManager.WanderPauseTime && !moving)
        {
            stateManager.Character.SetMovementDirection(PickRandomDirection());
            moving = true;
            timer = 0;
        }
    }

    public override void ExitState()
    {
    }

    private Vector3 PickRandomDirection()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        return new Vector3(rand.x, 0, rand.y);
    }

    public override string GetDebugName()
    {
        return "wander";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.CHASE_STATE, ToChaseState),
        };
    }

    private bool ToChaseState()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        HealthHandler playerHealth = stateManager.PlayerObject.GetComponent<HealthHandler>();
        return vecToPlayer.magnitude < stateManager.PlayerChaseDistance && playerHealth.CurHealth > 0;
    }
}
