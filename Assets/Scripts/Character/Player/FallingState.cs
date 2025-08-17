using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public override void EnterState()
    {
        InputActionsProvider.OnBButtonStarted += InputActionsProvider_OnBButtonStarted;
    }

    private void InputActionsProvider_OnBButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.SWORD_DOWN_SLASH_STATE;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        InputActionsProvider.OnBButtonStarted -= InputActionsProvider_OnBButtonStarted;
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "falling";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.LANDING_STATE, () => stateManager.Character.IsGrounded()),
            new StateTransition(PlayerStateManager.SWORD_DOWN_SLASH_STATE, () => stateManager.trigger == PlayerStateManager.SWORD_DOWN_SLASH_STATE),
            new StateTransition(PlayerStateManager.LEDGE_HANG_STATE, ToLedgeHang),
        };
    }

    private bool ToLedgeHang()
    {
        return stateManager.TimeInState > stateManager.MinLedgeGrabFallTime && stateManager.LedgeChecker.LedgeFound();
    }
}
