using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LandingState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "landing";
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.TimeInState > stateManager.LandingDuration || stateManager.DefaultMovementModule.MoveVec.magnitude > 0),
            new StateTransition(PlayerStateManager.IDLE_STATE, () => stateManager.TimeInState > stateManager.LandingDuration),
            new StateTransition(PlayerStateManager.FALLING_STATE, () => !stateManager.Character.IsGrounded()),
            new StateTransition(PlayerStateManager.ATTACK_STATE, () => stateManager.trigger == PlayerStateManager.ATTACK_STATE),
            new StateTransition(PlayerStateManager.JUMPING_STATE, () => stateManager.trigger == PlayerStateManager.JUMPING_STATE),
            new StateTransition(PlayerStateManager.ROLL_STATE, ToRollState),
            new StateTransition(PlayerStateManager.DODGING_STATE, ToDodgeState),
            new StateTransition(PlayerStateManager.CLIMBING_START_STATE, () => stateManager.trigger == PlayerStateManager.CLIMBING_START_STATE),
            new StateTransition(PlayerStateManager.LOOT_STATE, () => stateManager.trigger == PlayerStateManager.LOOT_STATE),
            new StateTransition(PlayerStateManager.BOW_DRAW_STATE, () => stateManager.trigger == PlayerStateManager.BOW_DRAW_STATE),
        };
    }

    private bool ToDodgeState()
    {
        return stateManager.trigger == PlayerStateManager.DODGE_TRIGGER && stateManager.Character.velocity.magnitude > 0 && stateManager.DefaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.Strafe;
    }

    private bool ToRollState()
    {
        return stateManager.trigger == PlayerStateManager.DODGE_TRIGGER && stateManager.Character.velocity.magnitude > 0 && stateManager.DefaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly;
    }
}
