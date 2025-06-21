using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class StateManager<TState> : StateManagerBase where TState : State
{
    public Dictionary<string, TState> registeredStates { get; private set; }
    private TState currentState;
    private string currentStateKey;
    private StateTransition[] currentTransitions;
    public delegate void StringFunc(string str);
    public event StringFunc OnStateChange;
    public string trigger; //reset at end of every frame to prevent unintentionally "queued" state changes
    public float TimeInState { get; private set; }
    public TState CurrentState
    {
        get
        {
            return currentState;
        }
    }
    [ShowInInspector, DisplayAsString]
    public string CurrentStateKey
    {
        get
        {
            return currentStateKey;
        }
    }

    private void Awake()
    {
        StartAwake();
        registeredStates = GetStateDictionary();

        string initialStateName = GetInitialStateName();
        SwitchState(initialStateName);
    }

    protected abstract string GetInitialStateName();
    protected abstract Dictionary<string, TState> GetStateDictionary();
    protected abstract void StartAwake();
    protected abstract void EndUpdate();

    private void Update()
    {
        TimeInState += Time.deltaTime;

        bool switched = false;
        foreach (StateTransition transition in currentTransitions)
        {
            if (transition.transitionCondition())
            {
                SwitchState(transition.targetStateName);
                switched = true;
                break;
            }
        }

        if (!switched) currentState.UpdateState();
        trigger = "";
        EndUpdate();
    }

    protected void SwitchState(string stateName)
    {
        if (!TryGetState(stateName, out TState state))
        {
            return;
        }
        if (state == currentState) return;
        if (currentState != null) currentState.ExitState();
        currentState = state;
        currentTransitions = currentState.GetTransitions();
        state.EnterState();
        currentStateKey = stateName;
        TimeInState = 0;
        OnStateChange?.Invoke(stateName);
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

    //useful for calling from UnityEvents and the inspector
    [Button]
    public void SetTrigger(string trigger)
    {
        this.trigger = trigger;
    }
}
