using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class IdleState : PlayerState
{
    //default state
    public override void EnterState()
    {
        Debug.Log("Entering idle");
        characterAdapter.canMove = true;
    }

    public override void UpdateState()
    {
        //temp input
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateManager.SwitchState("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            character.StopJumping();
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting idle");
    }

    protected override void PostInitialize()
    {
        Debug.Log("PostInitialize in idle");
        character.Jumped += Character_Jumped;
    }

    private void Character_Jumped()
    {
        stateManager.SwitchState("Jump");
    }
}
