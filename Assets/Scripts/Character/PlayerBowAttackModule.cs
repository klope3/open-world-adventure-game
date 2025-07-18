using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttackModule : MonoBehaviour
{
    [SerializeField] private PlayerStateManager stateManager;

    private void OnEnable()
    {
        InputActionsProvider.OnBButtonStarted += InputActionsProvider_OnBButtonStarted;
    }

    private void OnDisable()
    {
        InputActionsProvider.OnBButtonStarted -= InputActionsProvider_OnBButtonStarted;
    }

    private void InputActionsProvider_OnBButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.BOW_DRAW_STATE;
    }
}
