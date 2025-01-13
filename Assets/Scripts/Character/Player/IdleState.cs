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

        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    stateManager.SwitchState("Attack");
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    character.Jump();
        //} else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    character.StopJumping();
        //}
    }

    public override void ExitState()
    {
        Debug.Log("Exiting idle");
    }

    protected override void PostInitialize()
    {
        Debug.Log("PostInitialize in idle");
        InputActionsProvider.InputActions.Player.Jump.started += Jump_started;
        InputActionsProvider.InputActions.Player.Jump.canceled += Jump_canceled;

        InputActionsProvider.InputActions.Player.Attack.started += Attack_started;

        //character.Jumped += Character_Jumped;
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        stateManager.SwitchState("Attack");
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        character.StopJumping();
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        character.Jump();
        stateManager.SwitchState("Jump");
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        character.Jump();
        stateManager.SwitchState("Jump");
    }

    private void Character_Jumped()
    {
        stateManager.SwitchState("Jump");
    }
}
