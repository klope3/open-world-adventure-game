using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateManager : StateManager<CameraState>
{
    [field: SerializeField] public CameraLookOrbiter CameraLookOrbiter { get; private set; }
    [field: SerializeField] public TargetingPrioritizer TargetingPrioritizer { get; private set; }
    [field: SerializeField] public LookAtPosition CameraLookAtPosition { get; private set; }

    public static readonly string DEFAULT_STATE = "Default"; //typical "orbit player" state
    public static readonly string LOCKED_STATE = "Locked"; //orbiting is disabled, but no target is being targeted
    public static readonly string TARGETING_STATE = "Targeting";

    protected override void EndUpdate()
    {
    }

    protected override string GetInitialStateName()
    {
        return DEFAULT_STATE;
    }

    protected override Dictionary<string, CameraState> GetStateDictionary()
    {
        Dictionary<string, CameraState> states = new Dictionary<string, CameraState>();

        CameraDefaultState defaultState = new CameraDefaultState();
        CameraLockedState lockedState = new CameraLockedState();
        CameraTargetingState targetingState = new CameraTargetingState();

        defaultState.Initialize(this);
        lockedState.Initialize(this);
        targetingState.Initialize(this);

        states.Add(DEFAULT_STATE, defaultState);
        states.Add(LOCKED_STATE, lockedState);
        states.Add(TARGETING_STATE, targetingState);

        return states;
    }

    protected override void StartInitialize()
    {
    }
}
