using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHoldState : PlayerState
{
    private PlayerDefaultMovementModule.MovementType initialMovementType;

    public override void EnterState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.PlayerControlDataSO.BowMoveSpeed;
        stateManager.DefaultMovementModule.SetMovementType(PlayerDefaultMovementModule.MovementType.Strafe);
        stateManager.ShieldCollider.enabled = true;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        stateManager.DefaultMovementModule.SetMovementType(initialMovementType);
        stateManager.Character.maxWalkSpeed = stateManager.PlayerControlDataSO.DefaultMoveSpeed;
        stateManager.ShieldCollider.enabled = false;
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "shield hold";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.trigger == PlayerStateManager.MOVING_STATE),
        };
    }
}
