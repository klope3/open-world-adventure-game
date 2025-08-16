using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class DodgeState : PlayerState
{
    public System.Action OnEnter;

    public override void EnterState()
    {
        stateManager.cachedPlayerSpeed = stateManager.Character.maxWalkSpeed;
        stateManager.Character.maxWalkSpeed = 0;
        stateManager.cachedPlayerAcceleration = stateManager.Character.maxAcceleration;

        Vector2 snappedInputVec = InputActionsProvider.GetSnappedPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(snappedInputVec);
        
        stateManager.Character.maxAcceleration = 1000; //max mobility during dodge

        InputActionsProvider.OnBButtonStarted += InputActionsProvider_OnBButtonStarted;
        OnEnter?.Invoke();
    }

    private void InputActionsProvider_OnBButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.SWORD_SPIN_STATE;
    }

    public override void ExitState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.cachedPlayerSpeed;
        stateManager.Character.maxAcceleration = stateManager.cachedPlayerAcceleration;
        InputActionsProvider.UnlockPrimaryAxis();

        InputActionsProvider.OnBButtonStarted -= InputActionsProvider_OnBButtonStarted;
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        stateManager.Character.maxWalkSpeed = stateManager.CalculateDodgeMoveSpeed(stateManager.TimeInState);
    }

    public override string GetDebugName()
    {
        return "dodge";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.TimeInState >= stateManager.DodgeDuration),
            new StateTransition(PlayerStateManager.SWORD_SPIN_STATE, ToSwordSpin),
        };
    }

    private bool ToSwordSpin()
    {
        Vector2 snappedInputVec = InputActionsProvider.GetSnappedPrimaryAxis().normalized;
        bool correctTrigger = stateManager.trigger == PlayerStateManager.SWORD_SPIN_STATE;
        bool inWindow = stateManager.TimeInState < stateManager.SwordSpinWindow;
        bool isLeftOrRight = snappedInputVec.x > 0.98f || snappedInputVec.x < -0.98f;
        return correctTrigger && inWindow && isLeftOrRight;
    }
}
