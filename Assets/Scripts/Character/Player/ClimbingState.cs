using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class ClimbingState : PlayerState
{
    private Character.MovementMode initialMovementMode;
    private Character.RotationMode initialRotationMode;
    private float initialFlyingFriction;
    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        initialMovementMode = stateManager.Character.movementMode;
        initialRotationMode = stateManager.Character.rotationMode;
        initialFlyingFriction = stateManager.Character.flyingFriction;

        Vector3 surfaceNormal = stateManager.ClimbingDetector.GetSurfaceNormal();
        stateManager.Character.TeleportRotation(Quaternion.LookRotation(surfaceNormal * -1)); //snap to face climbing surface
        stateManager.Character.SetMovementMode(Character.MovementMode.Flying); //"flying" is the built-in ECM2 mode recommended by the dev for climbing-type movement
        stateManager.Character.SetRotationMode(Character.RotationMode.None);
        stateManager.Character.flyingFriction = 1000; //try to make sure momentum drift doesn't cause player position to become desynced with ladder rungs
        stateManager.ClimbingModule.enabled = true;
        stateManager.DefaultMovementModule.enabled = false;

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        stateManager.Character.SetMovementMode(initialMovementMode);
        stateManager.Character.SetRotationMode(initialRotationMode);
        stateManager.Character.flyingFriction = initialFlyingFriction;
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
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.trigger == PlayerStateManager.INTERACT_TRIGGER),
            new StateTransition(PlayerStateManager.CLIMBING_REACH_TOP_STATE, () => stateManager.trigger == PlayerStateManager.CLIMBING_REACH_TOP_STATE),
        };
    }
}
