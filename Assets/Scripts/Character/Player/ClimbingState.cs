using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class ClimbingState : PlayerState
{
    private PlayerClimbingModule climbingModule;
    private Character.MovementMode initialMovementMode;
    private Character.RotationMode initialRotationMode;
    private float initialFlyingFriction;
    private float initialFlySpeed;
    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        initialMovementMode = character.movementMode;
        initialRotationMode = character.rotationMode;
        initialFlySpeed = character.maxFlySpeed;
        initialFlyingFriction = character.flyingFriction;

        character.SetMovementMode(Character.MovementMode.Flying); //"flying" is the built-in ECM2 mode recommended by the dev for climbing-type movement
        character.SetRotationMode(Character.RotationMode.None);
        character.flyingFriction = 10;
        character.maxFlySpeed = 1.5f;
        climbingModule.enabled = true;
        characterAdapter.enabled = false;
        InputActionsProvider.OnInteractButtonStarted += InteractButton_started;

        OnEnter?.Invoke();
        //OnClimbingStart?.Invoke();
    }

    public override void ExitState()
    {
        character.SetMovementMode(initialMovementMode);
        character.SetRotationMode(initialRotationMode);
        character.flyingFriction = initialFlyingFriction;
        character.maxFlySpeed = initialFlySpeed;
        climbingModule.enabled = false;
        characterAdapter.enabled = true;
        InputActionsProvider.OnInteractButtonStarted -= InteractButton_started;

        OnExit?.Invoke();
        //OnClimbingEnd?.Invoke();
    }

    public override string GetDebugName()
    {
        return "climbing";
    }

    public void Initialize(PlayerStateManager stateManager, ECM2.Character character, ECM2CharacterAdapter characterAdapter, PlayerClimbingModule climbingModule)
    {
        Initialize(stateManager, character, characterAdapter);
        this.climbingModule = climbingModule;
    }

    public override void PostInitialize()
    {
    }

    private void InteractButton_started()
    {
        stateManager.SwitchState("Falling");
    }

    public override void UpdateState()
    {
    }
}
