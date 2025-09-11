using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a more general solution is needed for translating inputs into state manager triggers
public class PlayerShieldInput : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerStateManager;

    private void Awake()
    {
        InputActionsProvider.OnBlockButtonStarted += InputActionsProvider_OnBlockButtonStarted;
        InputActionsProvider.OnBlockButtonCanceled += InputActionsProvider_OnBlockButtonCanceled;
    }

    private void InputActionsProvider_OnBlockButtonStarted()
    {
        playerStateManager.trigger = PlayerStateManager.SHIELD_HOLD_STATE;
    }

    private void InputActionsProvider_OnBlockButtonCanceled()
    {
        playerStateManager.trigger = PlayerStateManager.MOVING_STATE;
    }
}
