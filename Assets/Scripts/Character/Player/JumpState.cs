using System.Collections;
using System.Collections.Generic;

public class JumpState : PlayerState
{
    private readonly float MIN_TIME = 0.1f; //in the first few frames, we don't want to "land" just because we're still close to the ground

    public override void EnterState()
    {
        stateManager.Character.Jump();
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {
        stateManager.jumpInput = false;
        stateManager.Character.StopJumping();
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "jump";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, ToDefaultState),
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.Character.velocity.y < 0),
        };
    }

    private bool ToDefaultState()
    {
        return stateManager.TimeInState > MIN_TIME && stateManager.Character.IsGrounded();
    }
}
