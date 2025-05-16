using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManagerInputs : MonoBehaviour
{
    [SerializeField] private PlayerStateManager stateManager;

    private void Awake()
    {
        InputActionsProvider.OnBButtonStarted += () => stateManager.trigger = PlayerStateManager.ATTACK_STATE;
        InputActionsProvider.OnAButtonStarted += () => stateManager.trigger = PlayerStateManager.JUMPING_STATE;
        InputActionsProvider.OnDodgeButtonStarted += () => stateManager.trigger = PlayerStateManager.ROLL_STATE;
    }
}
