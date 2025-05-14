using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class JumpState : PlayerState
{
    private readonly float MIN_TIME = 0.1f; //in the first few frames, we don't want to "land" just because we're still close to the ground
    //enter from pressing jump
    public override void EnterState()
    {
        stateManager.Character.Jump();
    }

    public override void UpdateState()
    {

    }

    //exit from releasing jump, or landing
    public override void ExitState()
    {
        stateManager.jumpInput = false;
        stateManager.Character.StopJumping();
    }

    public override void PostInitialize()
    {
        stateManager.Character.ReachedJumpApex += Character_ReachedJumpApex;
        stateManager.Character.Landed += Character_Landed; //edge case where we land on something before finishing jump process
        InputActionsProvider.OnAButtonCanceled += Jump_canceled;
    }

    private void Jump_canceled()
    {
        if (!stateManager.IsInState(this)) return;

        stateManager.Character.StopJumping();
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        //if (InputActionsProvider.GetPrimaryAxis().magnitude <= 0.005f) stateManager.ToDefaultState();
        //else stateManager.ToDefaultState();
    }

    private void Character_ReachedJumpApex()
    {
        //stateManager.SwitchState("Falling");
    }

    public override string GetDebugName()
    {
        return "jump";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, ToDefaultState),
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.Character.velocity.y < 0),
        };
    }

    private bool ToDefaultState()
    {
        return stateManager.TimeInState > MIN_TIME && stateManager.Character.IsGrounded();
    }
}
