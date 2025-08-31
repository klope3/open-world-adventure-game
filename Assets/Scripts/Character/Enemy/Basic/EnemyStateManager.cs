using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECM2;
using Sirenix.OdinInspector;

//this script is old and should be replaced with the new EnemyBasicStateManager soon
//along with all the states that go with it
public class EnemyStateManager : StateManager<EnemyState>
{
    [SerializeField] private Character character;
    [SerializeField] private HealthHandler healthHandler;
    [field: SerializeField] public GameObject PlayerObject { get; private set; }
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
    [SerializeField] private float hurtRecoveryTime;
    [SerializeField, ShowIf("@requireAimForAttack"), Range(0, 1), Tooltip("Lower values are more strict.")] private float aimTolerance;
    [SerializeField, Tooltip("If true, will not transition to the attack state unless it is roughly aimed at the player (among any other conditions).")] 
    private bool requireAimForAttack;
    [SerializeField] private MonoBehaviour wanderBehavior;
    [SerializeField] private MonoBehaviour chaseBehavior;
    [SerializeField] private MonoBehaviour hurtBehavior;
    [SerializeField] private MonoBehaviour pauseBehavior;
    [SerializeField] private MonoBehaviour attackBehavior;
    [SerializeField] private MonoBehaviour recoveryBehavior;

    public static readonly string WANDER_STATE = "Wander";
    public static readonly string CHASE_STATE = "Chase";
    public static readonly string ATTACK_STATE = "Attack";
    public static readonly string PAUSE_STATE = "Pause"; //really just an idle state
    public static readonly string RECOVERY_STATE = "Recovery";
    public static readonly string HURT_STATE = "Hurt";

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
    public float HurtRecoveryTime
    {
        get
        {
            return hurtRecoveryTime;
        }
    }
    public HealthHandler OwnHealth
    {
        get
        {
            return healthHandler;
        }
    }
    public bool RequireAimForAttack
    {
        get
        {
            return requireAimForAttack;
        }
    }
    public float AimTolerance
    {
        get
        {
            return aimTolerance;
        }
    }


    protected override void StartInitialize()
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
        Dictionary<string, EnemyState> states = new Dictionary<string, EnemyState>();

        EnemyBasicWanderState wanderState = new EnemyBasicWanderState();
        EnemyBasicChaseState chaseState = new EnemyBasicChaseState();
        EnemyBasicAttackState attackState = new EnemyBasicAttackState();
        EnemyBasicPauseState pauseState = new EnemyBasicPauseState();
        EnemyBasicRecoveryState recoveryState = new EnemyBasicRecoveryState();
        EnemyBasicHurtState hurtState = new EnemyBasicHurtState();

        wanderState.Initialize(this, wanderBehavior);
        chaseState.Initialize(this, chaseBehavior);
        attackState.Initialize(this, attackBehavior);
        pauseState.Initialize(this, pauseBehavior);
        recoveryState.Initialize(this, recoveryBehavior);
        hurtState.Initialize(this, hurtBehavior);

        states.Add(WANDER_STATE, wanderState);
        states.Add(CHASE_STATE, chaseState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(PAUSE_STATE, pauseState);
        states.Add(RECOVERY_STATE, recoveryState);
        states.Add(HURT_STATE, hurtState);

        return states;
    }

    public bool ShouldChasePlayer()
    {
        Vector3 vecToPlayer = PlayerObject.transform.position - OwnTransform.position;
        HealthHandler playerHealth = PlayerObject.GetComponent<HealthHandler>();
        return vecToPlayer.magnitude < PlayerChaseDistance && playerHealth.CurHealth > 0;
    }

    public void SetPlayer(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }
}
