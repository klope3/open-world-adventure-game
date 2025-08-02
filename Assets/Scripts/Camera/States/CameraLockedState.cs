using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockedState : CameraState
{
    public override void EnterState()
    {
        stateManager.CameraLookOrbiter.enabled = false;

        InputActionsProvider.OnZTargetStarted += InputActionsProvider_OnZTargetStarted;
    }

    private void InputActionsProvider_OnZTargetStarted()
    {
        stateManager.trigger = CameraStateManager.DEFAULT_STATE;
    }

    public override void ExitState()
    {
        InputActionsProvider.OnZTargetStarted -= InputActionsProvider_OnZTargetStarted;
    }

    public override string GetDebugName()
    {
        return "locked";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(CameraStateManager.DEFAULT_STATE, () => stateManager.trigger == CameraStateManager.DEFAULT_STATE),
        };
    }

    public override void UpdateState()
    {
    }
}
