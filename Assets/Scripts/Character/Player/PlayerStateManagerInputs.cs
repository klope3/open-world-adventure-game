using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManagerInputs : MonoBehaviour
{
    [SerializeField] private PlayerStateManager stateManager;

    private void OnEnable()
    {
        InputActionsProvider.OnAButtonStarted += InputActionsProvider_OnAButtonStarted;
        InputActionsProvider.OnDodgeButtonStarted += InputActionsProvider_OnDodgeButtonStarted;
    }

    private void OnDisable()
    {
        InputActionsProvider.OnAButtonStarted -= InputActionsProvider_OnAButtonStarted;
        InputActionsProvider.OnDodgeButtonStarted -= InputActionsProvider_OnDodgeButtonStarted;
    }

    private void InputActionsProvider_OnAButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.JUMPING_STATE;
    }

    private void InputActionsProvider_OnDodgeButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.DODGE_TRIGGER;
    }
}
