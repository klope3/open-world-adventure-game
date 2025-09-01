using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicWander : EnemyBasicState
{
    private float prevWalkSpeed;

    public override void EnterState()
    {
        prevWalkSpeed = stateManager.Character.maxWalkSpeed;
        stateManager.Character.maxWalkSpeed = stateManager.BehaviorData.WanderSpeed;
        stateManager.DirectionalMovement.enabled = true;
        stateManager.DirectionalMovement.RandomizeDirection();
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = prevWalkSpeed;
        stateManager.DirectionalMovement.enabled = false;
    }

    public override string GetDebugName()
    {
        return "wander";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyBasicStateManager.PAUSE_STATE, () => stateManager.TimeInState > stateManager.BehaviorData.WanderTime),
            new StateTransition(EnemyBasicStateManager.CHASE_STATE, () => stateManager.ShouldChaseTarget()),
            new StateTransition(EnemyBasicStateManager.DEATH_STATE, () => stateManager.HealthHandler.CurHealth <= 0),
        };
    }

    public override void UpdateState()
    {

    }
}
