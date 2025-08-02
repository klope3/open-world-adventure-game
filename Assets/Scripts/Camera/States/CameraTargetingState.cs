using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetingState : CameraState
{
    public override void EnterState()
    {
        stateManager.CameraLookOrbiter.enabled = false;
        stateManager.CameraLookAtPosition.enabled = true;
        stateManager.TargetingPrioritizer.enabled = false; //don't change prioritization while we're already locked onto a target
        stateManager.CameraLookAtPosition.lookAtTransform = stateManager.TargetingPrioritizer.ObjectToBeTargeted.transform;

        InputActionsProvider.OnZTargetStarted += InputActionsProvider_OnZTargetStarted;
    }

    private void InputActionsProvider_OnZTargetStarted()
    {
        stateManager.trigger = CameraStateManager.DEFAULT_STATE;
    }

    public override void ExitState()
    {
        stateManager.CameraLookAtPosition.enabled = false;
        stateManager.TargetingPrioritizer.enabled = true;
        Vector3 finalLookVector = Quaternion.LookRotation(stateManager.CameraLookAtPosition.GetLookVector()).eulerAngles;
        stateManager.CameraLookOrbiter.SetCameraAngle(finalLookVector);

        InputActionsProvider.OnZTargetStarted -= InputActionsProvider_OnZTargetStarted;
    }

    public override string GetDebugName()
    {
        return "targeting";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(CameraStateManager.DEFAULT_STATE, ToDefaultState),
        };
    }

    private bool ToDefaultState()
    {
        return stateManager.TargetingPrioritizer.ObjectToBeTargeted == null || stateManager.trigger == CameraStateManager.DEFAULT_STATE;
    }

    public override void UpdateState()
    {
    }
}
