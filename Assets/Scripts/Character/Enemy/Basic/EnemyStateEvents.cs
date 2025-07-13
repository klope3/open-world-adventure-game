using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateEvents : MonoBehaviour
{
    [SerializeField] private EnemyStateManager stateManager;
    public UnityEvent OnAttack;
    public UnityEvent OnChase;
    public UnityEvent OnPause;
    public UnityEvent OnRecovery;
    public UnityEvent OnWander;
    public UnityEvent OnHurt;

    private void OnEnable()
    {
        stateManager.OnStateChange += StateManager_OnStateChange;
    }


    private void OnDisable()
    {
        stateManager.OnStateChange -= StateManager_OnStateChange;
    }
    private void StateManager_OnStateChange(string newState, string prevState)
    {
        if (newState == EnemyStateManager.WANDER_STATE) OnWander?.Invoke();
        if (newState == EnemyStateManager.CHASE_STATE) OnChase?.Invoke();
        if (newState == EnemyStateManager.PAUSE_STATE) OnPause?.Invoke();
        if (newState == EnemyStateManager.RECOVERY_STATE) OnRecovery?.Invoke();
        if (newState == EnemyStateManager.ATTACK_STATE) OnAttack?.Invoke();
        if (newState == EnemyStateManager.HURT_STATE) OnHurt?.Invoke();
    }
}
