using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class StateManager<TState> : MonoBehaviour where TState : State
{
    public Dictionary<string, TState> registeredStates { get; private set; }
    private TState currentState;
    public float TimeInState { get; private set; }
    public TState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    private void Awake()
    {
        StartAwake();
        registeredStates = GetStateDictionary();

        string initialStateName = GetInitialStateName();
        if (!TryGetState(initialStateName, out TState initialState))
        {
            return;
        }
        currentState = initialState;
        currentState.EnterState();
    }

    protected abstract string GetInitialStateName();
    protected abstract Dictionary<string, TState> GetStateDictionary();
    protected abstract void StartAwake();
    protected abstract void EndUpdate();

    private void Update()
    {
        TimeInState += Time.deltaTime;
        currentState.UpdateState();
        EndUpdate();
    }

    public void SwitchState(string stateName)
    {
        if (!TryGetState(stateName, out TState state))
        {
            return;
        }
        if (state == currentState) return;
        currentState.ExitState();
        currentState = state;
        state.EnterState();
        TimeInState = 0;
    }

    private bool TryGetState(string name, out TState state)
    {
        if (!registeredStates.TryGetValue(name, out state))
        {
            Debug.LogError($"State with name '{name}' not registered!");
            return false;
        }

        return true;
    }
}
