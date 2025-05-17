using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class ClimbingState : PlayerState
{
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
        stateManager.DefaultMovementModule.enabled = false;

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        stateManager.Character.SetMovementMode(initialMovementMode);
        stateManager.Character.SetRotationMode(initialRotationMode);
        stateManager.Character.flyingFriction = initialFlyingFriction;
        stateManager.Character.maxFlySpeed = initialFlySpeed;
        stateManager.ClimbingModule.enabled = false;
        stateManager.DefaultMovementModule.enabled = true;

        OnExit?.Invoke();
    }

    public override string GetDebugName()
    {
        return "climbing";
    }

    public override void PostInitialize()
    {
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
