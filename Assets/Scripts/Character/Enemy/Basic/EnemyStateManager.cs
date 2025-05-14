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
    public GameObject PlayerObject { get; private set; }
    //public System.Action OnAttack;
    //public System.Action OnPause;
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

    public static readonly string WANDER_STATE = "Wander";
    public static readonly string CHASE_STATE = "Chase";
    public static readonly string ATTACK_STATE = "Attack";
    public static readonly string PAUSE_STATE = "Pause";

    public Character Character
    {
        get
        {
            return character;
        }
    }
    public Transform OwnTransform
    {
        get
        {
            return transform;
        }
    }
    public float PlayerChaseDistance
    {
        get
        {
            return playerChaseDistance;
        }
    }
    public float ChaseSpeed
    {
        get
        {
            return chaseSpeed;
        }
    }
    public float WanderSpeed
    {
        get
        {
            return wanderMoveSpeed;
        }
    }
    public float WanderMoveTime
    {
        get
        {
            return wanderMoveTime;
        }
    }
    public float WanderPauseTime
    {
        get
        {
            return wanderPauseTime;
        }
    }
    public float AttackProximity
    {
        get
        {
            return attackProximity;
        }
    }
    public float AttackDuration
    {
        get
        {
            return attackDuration;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
    }
    public float AttackRecoveryDuration
    {
        get
        {
            return attackRecoveryDuration;
        }
    }
    public HealthHandler OwnHealth
    {
        get
        {
            return healthHandler;
        }
    }


    protected override void StartAwake()
    {
    }

    protected override void EndUpdate()
    {
    }

    protected override string GetInitialStateName()
    {
        return WANDER_STATE;
    }

    protected override Dictionary<string, EnemyState> GetStateDictionary()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (!PlayerObject)
        {
            Debug.LogError("Could not find player!");
        }

        Dictionary<string, EnemyState> states = new Dictionary<string, EnemyState>();

        EnemyBasicWanderState wanderState = new EnemyBasicWanderState();
        EnemyBasicChaseState chaseState = new EnemyBasicChaseState();
        EnemyBasicAttackState attackState = new EnemyBasicAttackState();
        EnemyBasicPauseState pauseState = new EnemyBasicPauseState();

        //wanderState.Initialize(this, character, playerObj, transform, wanderMoveSpeed, wanderMoveTime, wanderPauseTime, playerChaseDistance);
        //chaseState.Initialize(this, character, playerObj, transform, playerChaseDistance, chaseSpeed, attackProximity);
        //attackState.Initialize(this, character, playerObj, transform, healthHandler, attackSpeed, attackDuration);
        //pauseState.Initialize(this, character, playerObj, transform, attackRecoveryDuration);
        wanderState.Initialize_NEW(this);
        chaseState.Initialize_NEW(this);
        attackState.Initialize_NEW(this);
        pauseState.Initialize_NEW(this);

        //attackState.OnEnter += AttackState_OnEnter;
        //attackState.OnExit += AttackState_OnExit;
        //pauseState.OnEnter += PauseState_OnEnter;

        states.Add(WANDER_STATE, wanderState);
        states.Add(CHASE_STATE, chaseState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(PAUSE_STATE, pauseState);

        return states;
    }

    //private void PauseState_OnEnter()
    //{
    //    OnPause?.Invoke();
    //}
    //
    //private void AttackState_OnEnter()
    //{
    //    OnAttack?.Invoke();
    //    OnAttackStart?.Invoke();
    //}
    //
    //private void AttackState_OnExit()
    //{
    //    OnAttackEnd?.Invoke();
    //}
}
