using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class ClimbingState : PlayerState
{
    //private PlayerClimbingModule climbingModule;
    private Character.MovementMode initialMovementMode;
    private Character.RotationMode initialRotationMode;
    private float initialFlyingFriction;
    private float initialFlySpeed;
    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        initialMovementMode = stateManager.Character.movementMode;
        initialRotationMode = stateManager.Character.rotationMode;
        initialFlySpeed = stateManager.Character.maxFlySpeed;
        initialFlyingFriction = stateManager.Character.flyingFriction;

        stateManager.Character.SetMovementMode(Character.MovementMode.Flying); //"flying" is the built-in ECM2 mode recommended by the dev for climbing-type movement
        stateManager.Character.SetRotationMode(Character.RotationMode.None);
        stateManager.Character.flyingFriction = 10;
        stateManager.Character.maxFlySpeed = 1.5f;
        stateManager.ClimbingModule.enabled = true;
        stateManager.CharacterAdapter.enabled = false;
        InputActionsProvider.OnInteractButtonStarted += InteractButton_started;

        OnEnter?.Invoke();
        //OnClimbingStart?.Invoke();
    }

    public override void ExitState()
    {
        stateManager.Character.SetMovementMode(initialMovementMode);
        stateManager.Character.SetRotationMode(initialRotationMode);
        stateManager.Character.flyingFriction = initialFlyingFriction;
        stateManager.Character.maxFlySpeed = initialFlySpeed;
        stateManager.ClimbingModule.enabled = false;
        stateManager.CharacterAdapter.enabled = true;
        InputActionsProvider.OnInteractButtonStarted -= InteractButton_started;

        OnExit?.Invoke();
        //OnClimbingEnd?.Invoke();
    }

    public override string GetDebugName()
    {
        return "climbing";
    }

    //public void Initialize(PlayerStateManager stateManager, ECM2.Character character, ECM2CharacterAdapter characterAdapter, PlayerClimbingModule climbingModule)
    //{
    //    Initialize(stateManager, character, characterAdapter);
    //    this.climbingModule = climbingModule;
    //}

    public override void PostInitialize()
    {
    }

    private void InteractButton_started()
    {
        //stateManager.SwitchState("Falling");
    }

    public override void UpdateState()
    {
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.FALLING_STATE, ToFallingState),
        };
    }

    private bool ToFallingState()
    {
        return false;
    }
}
