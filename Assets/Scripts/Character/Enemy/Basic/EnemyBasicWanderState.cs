using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicWanderState : EnemyState
{

    public override void EnterState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.WanderSpeed;
        stateManager.Character.SetMovementDirection(PickRandomDirection());
    }

    public override void UpdateState()
    {
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
            new StateTransition(EnemyStateManager.HURT_STATE, () => stateManager.trigger == EnemyStateManager.HURT_STATE),
            new StateTransition(EnemyStateManager.CHASE_STATE, () => stateManager.ShouldChasePlayer()),
            new StateTransition(EnemyStateManager.PAUSE_STATE, () => stateManager.TimeInState >= stateManager.WanderMoveTime),
        };
    }
}
