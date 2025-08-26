using System.Collections;
using System.Collections.Generic;

public class MovingState : PlayerState
{
    public override void EnterState()
    {
        InputActionsProvider.OnAButtonStarted += InputActionsProvider_OnAButtonStarted;
        InputActionsProvider.OnDodgeButtonStarted += InputActionsProvider_OnDodgeButtonStarted;
    }

    private void InputActionsProvider_OnAButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.JUMPING_STATE;
    }
    
    private void InputActionsProvider_OnDodgeButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.DODGE_TRIGGER;
    }

    public override void ExitState()
    {
        InputActionsProvider.OnAButtonStarted -= InputActionsProvider_OnAButtonStarted;
        InputActionsProvider.OnDodgeButtonStarted -= InputActionsProvider_OnDodgeButtonStarted;
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "moving";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.IDLE_STATE, () => stateManager.DefaultMovementModule.MoveVec.magnitude < 0.005f),
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
