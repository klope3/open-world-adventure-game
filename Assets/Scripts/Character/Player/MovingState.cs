using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class MovingState : PlayerState
{
    private InteractionZone interactionZone;
    private CameraController cameraController;

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void PostInitialize()
    {
        InputActionsProvider.OnDodgeButtonStarted += DodgeButton_started;
        InputActionsProvider.OnAButtonStarted += Jump_started;
        InputActionsProvider.OnBButtonStarted += Attack_started;
        InputActionsProvider.OnInteractButtonStarted += InteractButton_started;
        InputActionsProvider.OnZTargetStarted += ZTarget_started;
    }

    public override void UpdateState()
    {
        if (InputActionsProvider.GetPrimaryAxis().magnitude <= 0.005f)
        {
            stateManager.SwitchState("Idle");
            return;
        }
    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, InteractionZone interactionZone, CameraController cameraController)
    {
        Initialize(stateManager, character, characterAdapter);
        this.interactionZone = interactionZone;
        this.cameraController = cameraController;
    }

    private void ZTarget_started()
    {
        if (stateManager.IsInState(this)) cameraController.ToggleTargeting();
    }

    private void InteractButton_started()
    {
        if (stateManager.IsInState(this)) interactionZone.Interact();
    }

    private void Attack_started()
    {
        if (!stateManager.IsInState(this)) return;

        stateManager.SwitchState("Attack");
    }

    private void Jump_started()
    {
        if (!stateManager.IsInState(this)) return;

        character.Jump();
        stateManager.SwitchState("Jump");
    }

    private void DodgeButton_started()
    {
        if (!stateManager.IsInState(this)) return;

        if (!cameraController.IsTargeting)
        {
            stateManager.SwitchState("Roll");
            return;
        } else
        {
            stateManager.SwitchState("Dodge");
        }


    }
}
