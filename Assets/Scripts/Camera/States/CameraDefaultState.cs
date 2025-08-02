using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDefaultState : CameraState
{
    public override void EnterState()
    {
        stateManager.CameraLookOrbiter.enabled = true;

        InputActionsProvider.OnZTargetStarted += InputActionsProvider_OnZTargetStarted;
    }

    private void InputActionsProvider_OnZTargetStarted()
    {
        stateManager.trigger = stateManager.TargetingPrioritizer.ObjectToBeTargeted ? CameraStateManager.TARGETING_STATE : CameraStateManager.LOCKED_STATE;
    }

    public override void ExitState()
    {
        InputActionsProvider.OnZTargetStarted -= InputActionsProvider_OnZTargetStarted;
    }

    public override string GetDebugName()
    {
        return "default";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(CameraStateManager.TARGETING_STATE, () => stateManager.trigger == CameraStateManager.TARGETING_STATE),
            new StateTransition(CameraStateManager.LOCKED_STATE, () => stateManager.trigger == CameraStateManager.LOCKED_STATE),
        };
    }

    public override void UpdateState()
    {
    }
}
