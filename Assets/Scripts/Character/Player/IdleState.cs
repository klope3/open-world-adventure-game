using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

//note that IdleState is currently used for both standing idle and normal walking/running around
public class IdleState : PlayerState
{
    private InteractionZone interactionZone;
    private CameraController cameraController;

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

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, InteractionZone interactionZone, CameraController cameraController)
    {
        Initialize(stateManager, character, characterAdapter);
        this.interactionZone = interactionZone;
        this.cameraController = cameraController;
    }

    protected override void PostInitialize()
    {
        Debug.Log("PostInitialize in idle");
        InputActionsProvider.InputActions.Player.AButton.started += Jump_started;

        InputActionsProvider.InputActions.Player.BButton.started += Attack_started;

        InputActionsProvider.InputActions.Player.InteractButton.started += InteractButton_started;

        InputActionsProvider.InputActions.Player.ZTarget.started += ZTarget_started;
        //character.Jumped += Character_Jumped;
    }

    private void ZTarget_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (stateManager.IsInState(this)) cameraController.ToggleTargeting();
    }

    private void InteractButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (stateManager.IsInState(this)) interactionZone.Interact();
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!stateManager.IsInState(this)) return;

        stateManager.SwitchState("Attack");
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!stateManager.IsInState(this)) return;

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
