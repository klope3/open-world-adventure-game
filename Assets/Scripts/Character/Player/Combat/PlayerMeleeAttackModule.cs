using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackModule : MonoBehaviour
{
    [SerializeField] private PlayerStateManager stateManager;
    [SerializeField] private PlayerControlDataSO playerControlData;
    private float timeSinceLastAttack;
    private string lastAttackKey;

    private void StateManager_OnStateChange(string newState, string prevState)
    {
        if (newState == PlayerStateManager.ATTACK_STATE || newState == PlayerStateManager.ATTACK2_STATE)
        {
            lastAttackKey = newState;
            timeSinceLastAttack = 0;
        }
    }

    private void Update()
    {
        if (timeSinceLastAttack > playerControlData.StandardAttackMaxChainTime) return; //we don't need to keep incrementing if the attack-chaining window has already passed
        timeSinceLastAttack += Time.deltaTime;
    }

    private void OnEnable()
    {
        InputActionsProvider.OnBButtonStarted += InputActionsProvider_OnBButtonStarted;
        stateManager.OnStateChange += StateManager_OnStateChange;
    }

    private void OnDisable()
    {
        InputActionsProvider.OnBButtonStarted -= InputActionsProvider_OnBButtonStarted;
        stateManager.OnStateChange -= StateManager_OnStateChange;
    }

    private void InputActionsProvider_OnBButtonStarted()
    {
        bool inChainingWindow = timeSinceLastAttack > playerControlData.StandardAttackMinChainTime && timeSinceLastAttack < playerControlData.StandardAttackMaxChainTime;
        if (inChainingWindow)
        {
            if (lastAttackKey == PlayerStateManager.ATTACK_STATE) stateManager.trigger = PlayerStateManager.ATTACK2_STATE;
            else stateManager.trigger = PlayerStateManager.ATTACK_STATE;
            return;
        }
        stateManager.trigger = PlayerStateManager.ATTACK_STATE;
    }
}
