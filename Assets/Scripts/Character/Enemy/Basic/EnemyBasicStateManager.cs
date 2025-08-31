using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBasicStateManager : StateManager<EnemyBasicState>
{
    [field: SerializeField] public EnemyBasicBehaviorDataSO BehaviorData;
    [field: SerializeField] public DirectionalMovement DirectionalMovement;
    [field: SerializeField] public ChaseGameObject ChaseGameObject;
    [field: SerializeField] public HealthHandler HealthHandler;
    [SerializeField, Tooltip("Objects detected by this zone will be attacked.")] private GameObjectDetectorZone targetDetectorZone;
    public GameObject Target { get; private set; }
    public UnityEvent OnAttackStart;

    public static readonly string WANDER_STATE = "wander";
    public static readonly string PAUSE_STATE = "pause";
    public static readonly string CHASE_STATE = "chase";
    public static readonly string ATTACK_STATE = "attack";
    public static readonly string DEATH_STATE = "death";

    protected override void EndUpdate()
    {
    }

    protected override string GetInitialStateName()
    {
        return PAUSE_STATE;
    }

    protected override Dictionary<string, EnemyBasicState> GetStateDictionary()
    {
        Dictionary<string, EnemyBasicState> states = new Dictionary<string, EnemyBasicState>()
        {
            { PAUSE_STATE, new EnemyBasicPause() },
            { WANDER_STATE, new EnemyBasicWander() },
            { CHASE_STATE, new EnemyBasicChase() },
            { ATTACK_STATE, new EnemyBasicAttack() },
            { DEATH_STATE, new EnemyBasicDeath() },
        };

        foreach (KeyValuePair<string, EnemyBasicState> state in states)
        {
            state.Value.Initialize(this);
        }

        return states;
    }

    protected override void StartInitialize()
    {
        targetDetectorZone.OnObjectEntered += TargetDetectorZone_OnObjectEntered;
        targetDetectorZone.OnObjectExited += TargetDetectorZone_OnObjectExited;
    }

    private void TargetDetectorZone_OnObjectExited(GameObject obj)
    {
        if (Target == obj) Target = null;
    }

    private void TargetDetectorZone_OnObjectEntered(GameObject obj)
    {
        Target = obj;
    }

    public bool ShouldChaseTarget()
    {
        return Target != null;
    }

    public void Attack()
    {
        OnAttackStart?.Invoke();
    }

    public void ClearTarget()
    {
        Target = null;
    }
}
