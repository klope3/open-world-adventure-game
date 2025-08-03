using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : PlayerState
{
    public System.Action OnEnter;

    public override void EnterState()
    {
        stateManager.cachedPlayerSpeed = stateManager.Character.maxWalkSpeed;
        stateManager.Character.maxWalkSpeed = 0;
        stateManager.cachedPlayerAcceleration = stateManager.Character.maxAcceleration;
        
        Vector2 inputVec = InputActionsProvider.GetPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(inputVec);
        
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
        return stateManager.trigger == PlayerStateManager.SWORD_SPIN_STATE && stateManager.TimeInState < stateManager.SwordSpinWindow;
    }
}
