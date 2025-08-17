using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "idle";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.DefaultMovementModule.MoveVec.magnitude >= 0.005f),
            new StateTransition(PlayerStateManager.FALLING_STATE, () => !stateManager.Character.IsGrounded()),
            new StateTransition(PlayerStateManager.ATTACK_STATE, () => stateManager.trigger == PlayerStateManager.ATTACK_STATE),
            new StateTransition(PlayerStateManager.JUMPING_STATE, () => stateManager.trigger == PlayerStateManager.JUMPING_STATE),
            new StateTransition(PlayerStateManager.CLIMBING_START_STATE, () => stateManager.trigger == PlayerStateManager.CLIMBING_START_STATE),
            new StateTransition(PlayerStateManager.LOOT_STATE, () => stateManager.trigger == PlayerStateManager.LOOT_STATE),
            new StateTransition(PlayerStateManager.BOW_DRAW_STATE, () => stateManager.trigger == PlayerStateManager.BOW_DRAW_STATE),
            new StateTransition(PlayerStateManager.LEDGE_HANG_STATE, () => stateManager.trigger == PlayerStateManager.LEDGE_HANG_STATE), //TEMP
        };
    }
}
