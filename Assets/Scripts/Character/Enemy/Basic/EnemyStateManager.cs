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
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

    public static readonly string WANDER_STATE = "Wander";
    public static readonly string CHASE_STATE = "Chase";
    public static readonly string ATTACK_STATE = "Attack";
    public static readonly string PAUSE_STATE = "Pause";
    public static readonly string RECOVERY_STATE = "Recovery";

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
        return PAUSE_STATE;
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
        EnemyBasicRecoveryState recoveryState = new EnemyBasicRecoveryState();

        wanderState.Initialize(this);
        chaseState.Initialize(this);
        attackState.Initialize(this);
        pauseState.Initialize(this);
        recoveryState.Initialize(this);

        states.Add(WANDER_STATE, wanderState);
        states.Add(CHASE_STATE, chaseState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(PAUSE_STATE, pauseState);
        states.Add(RECOVERY_STATE, recoveryState);

        return states;
    }

    public bool ShouldChasePlayer()
    {
        Vector3 vecToPlayer = PlayerObject.transform.position - OwnTransform.position;
        HealthHandler playerHealth = PlayerObject.GetComponent<HealthHandler>();
        return vecToPlayer.magnitude < PlayerChaseDistance && playerHealth.CurHealth > 0;
    }
}
