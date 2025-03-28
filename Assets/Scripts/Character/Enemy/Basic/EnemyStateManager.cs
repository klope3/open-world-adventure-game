using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECM2;

public class EnemyStateManager : StateManager<EnemyState>
{
    [SerializeField] private Character character;
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private float wanderMoveSpeed;

    [SerializeField, Tooltip("The enemy will move for this long before pausing.")] 
    private float wanderMoveTime;

    [SerializeField, Tooltip("The enemy will pause for this long after moving.")]
    private float wanderPauseTime;

    [SerializeField] private float playerChaseDistance;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackProximity;
    [SerializeField] private float attackRecoveryDuration;
    public System.Action OnAttack;
    public System.Action OnPause;
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;


    protected override void StartAwake()
    {
    }

    protected override void EndUpdate()
    {
    }

    protected override string GetInitialStateName()
    {
        return "Wander";
    }

    protected override Dictionary<string, EnemyState> GetStateDictionary()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (!playerObj)
        {
            Debug.LogError("Could not find player!");
        }

        Dictionary<string, EnemyState> states = new Dictionary<string, EnemyState>();

        EnemyBasicWanderState wanderState = new EnemyBasicWanderState();
        EnemyBasicChaseState chaseState = new EnemyBasicChaseState();
        EnemyBasicAttackState attackState = new EnemyBasicAttackState();
        EnemyBasicPauseState pauseState = new EnemyBasicPauseState();
        
        wanderState.Initialize(this, character, playerObj, transform, wanderMoveSpeed, wanderMoveTime, wanderPauseTime, playerChaseDistance);
        chaseState.Initialize(this, character, playerObj, transform, playerChaseDistance, chaseSpeed, attackProximity);
        attackState.Initialize(this, character, playerObj, transform, healthHandler, attackSpeed, attackDuration);
        pauseState.Initialize(this, character, playerObj, transform, attackRecoveryDuration);

        attackState.OnEnter += AttackState_OnEnter;
        attackState.OnExit += AttackState_OnExit;
        pauseState.OnEnter += PauseState_OnEnter;

        states.Add("Wander", wanderState);
        states.Add("Chase", chaseState);
        states.Add("Attack", attackState);
        states.Add("Pause", pauseState);

        return states;
    }

    private void PauseState_OnEnter()
    {
        OnPause?.Invoke();
    }

    private void AttackState_OnEnter()
    {
        OnAttack?.Invoke();
        OnAttackStart?.Invoke();
    }

    private void AttackState_OnExit()
    {
        OnAttackEnd?.Invoke();
    }
}
