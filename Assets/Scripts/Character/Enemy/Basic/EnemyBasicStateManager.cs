using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBasicStateManager : StateManager<EnemyBasicState>
{
    [field: SerializeField] public EnemyBasicBehaviorDataSO BehaviorData { get; private set; }
    [field: SerializeField] public DirectionalMovement DirectionalMovement { get; private set; }
    [field: SerializeField] public ChaseGameObject ChaseGameObject { get; private set; }
    [field: SerializeField] public HealthHandler HealthHandler { get; private set; }
    [field: SerializeField] public ECM2.Character Character { get; private set; }
    [field: SerializeField] public EnemyBasicTargetHandler TargetHandler { get; private set; }
    public UnityEvent OnAttackStart;

    public static readonly string WANDER_STATE = "wander";
    public static readonly string PAUSE_STATE = "pause";
    public static readonly string CHASE_STATE = "chase";
    public static readonly string ATTACK_STATE = "attack";
    public static readonly string ATTACK_RECOVERY_STATE = "attack recovery";
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
            { ATTACK_RECOVERY_STATE, new EnemyBasicAttackRecovery() },
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
    }

    public bool ShouldChaseTarget()
    {
        return TargetHandler.Target != null;
    }

    public void Attack()
    {
        OnAttackStart?.Invoke();
    }
}
