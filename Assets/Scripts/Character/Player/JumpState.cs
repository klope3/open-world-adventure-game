using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class JumpState : PlayerState
{
    //enter from pressing jump
    public override void EnterState()
    {
        Debug.Log("Entering jump");
    }

    public override void UpdateState()
    {

    }

    //exit from releasing jump, or landing
    public override void ExitState()
    {
        Debug.Log("Exiting jump");
    }

    public override void PostInitialize()
    {
        character.ReachedJumpApex += Character_ReachedJumpApex;
        character.Landed += Character_Landed; //edge case where we land on something before finishing jump process
        InputActionsProvider.OnAButtonCanceled += Jump_canceled;
    }

    private void Jump_canceled()
    {
        if (!stateManager.IsInState(this)) return;

        character.StopJumping();
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        stateManager.SwitchState("Idle");
    }

    private void Character_ReachedJumpApex()
    {
        stateManager.SwitchState("Falling");
    }
}
