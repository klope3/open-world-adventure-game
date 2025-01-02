using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyStateManager : StateManager<EnemyState>
{
    [SerializeField] private Character character;
    [SerializeField] private float wanderMoveSpeed;
    [SerializeField, Tooltip("The enemy will move for this long, then pause for this long, then repeat.")] 
    private float wanderMoveTime;
    [SerializeField] private float playerChaseDistance;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackProximity;
    [SerializeField] private float attackRecoveryDuration;
    public System.Action OnAttack;

    protected override void StartAwake()
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
        
        wanderState.Initialize(this, character, playerObj, transform, wanderMoveSpeed, wanderMoveTime, playerChaseDistance);
        chaseState.Initialize(this, character, playerObj, transform, playerChaseDistance, chaseSpeed, attackProximity);
        attackState.Initialize(this, character, playerObj, transform, attackSpeed, attackDuration);
        pauseState.Initialize(this, character, playerObj, transform, attackRecoveryDuration);

        attackState.OnEnter += AttackState_OnEnter;

        states.Add("Wander", wanderState);
        states.Add("Chase", chaseState);
        states.Add("Attack", attackState);
        states.Add("Pause", pauseState);

        return states;
    }

    private void AttackState_OnEnter()
    {
        OnAttack?.Invoke();
    }
}
