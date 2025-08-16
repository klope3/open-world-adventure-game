using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using UnityEngine.Events;

public class PlayerStateManager : StateManager<PlayerState>
{
    [SerializeField] private PlayerDefaultMovementModule defaultMovementModule;
    [SerializeField] private Character character;
    [SerializeField] private InteractionZone interactionZone;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private InputActionsEvents inputActionsEvents;
    [SerializeField] private RaycastChecker climbingDetector;
    [SerializeField] private PlayerClimbingModule climbingModule;
    [SerializeField] private MegaProjectileLauncher arrowLauncher;
    [field: SerializeField] public DamageZone SwordDamageZone { get; private set; }
    [SerializeField] private float standardAttackDuration;
    [SerializeField, Tooltip("The player must press attack at most this long after the previous attack state finished in order to chain the next attack.")]
    private float standardAttackChainTime;
    [SerializeField] private float rollDuration;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDeceleration;
    [SerializeField] private float dodgeDuration;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeDeceleration;
    [SerializeField] private float landingDuration;
    [SerializeField] private float climbingReachTopDuration;
    [SerializeField] private float climbingStartDuration;
    [SerializeField] private float bowDrawDuration;
    [field: SerializeField] public float SwordSpinDuration { get; private set; }
    [field: SerializeField] public float SwordUpSlashDuration { get; private set; }
    [field: SerializeField] public float SwordDownSlashDuration { get; private set; }
    [field: SerializeField] public float SwordDownSlashBounceAmount { get; private set; }
    [field: SerializeField, Tooltip("The time window in which pressing attack during a dodge will trigger a sword spin.")] 
    public float SwordSpinWindow { get; private set; }
    [HideInInspector] public float cachedPlayerSpeed; //useful for when player speed needs to be reset to a previous value after multiple state transitions
    [HideInInspector] public float cachedPlayerAcceleration;
    private int recentStandardAttacks; //increments while chaining attacks; resets to 0 when standardAttackChainTime elapses
    public System.Action OnDefaultState;
    public System.Action OnAttack2;
    public System.Action OnLeftGround;
    public System.Action OnDodge;
    public System.Action OnClimbingStart;
    public System.Action OnClimbingStop;
    public System.Action OnLand;

    public bool attackInput;
    public bool jumpInput;

    public UnityEvent OnAnyAttackStart;
    public UnityEvent OnAnyAttackEnd;
    public UnityEvent OnLanded;

    public static readonly string IDLE_STATE = "Idle";
    public static readonly string MOVING_STATE = "Moving";
    public static readonly string ROLL_STATE = "Rolling";
    public static readonly string ATTACK_STATE = "Attacking";
    public static readonly string ATTACK2_STATE = "Attacking2";
    public static readonly string FALLING_STATE = "Falling";
    public static readonly string DODGING_STATE = "Dodging";
    public static readonly string JUMPING_STATE = "Jumping";
    public static readonly string CLIMBING_STATE = "Climbing";
    public static readonly string DYING_STATE = "Dying";
    public static readonly string LANDING_STATE = "Landing";
    public static readonly string CLIMBING_REACH_TOP_STATE = "Climbing reach top";
    public static readonly string CLIMBING_START_STATE = "Climbing start";
    public static readonly string INTERACT_TRIGGER = "Interact";
    public static readonly string DODGE_TRIGGER = "Dodge";
    public static readonly string LOOT_STATE = "Loot";
    public static readonly string BOW_DRAW_STATE = "Bow Draw";
    public static readonly string BOW_HOLD_STATE = "Bow Hold";
    public static readonly string SWORD_SPIN_STATE = "Sword Spin";
    public static readonly string SWORD_UP_SLASH_STATE = "Sword Up Slash";
    public static readonly string SWORD_DOWN_SLASH_STATE = "Sword Down Slash";

    public float ClimbingReachTopDuration
    {
        get
        {
            return climbingReachTopDuration;
        }
    }
    public PlayerDefaultMovementModule DefaultMovementModule
    {
        get
        {
            return defaultMovementModule;
        }
    }
    public Character Character
    {
        get
        {
            return character;
        }
    }
    public PlayerClimbingModule ClimbingModule
    {
        get
        {
            return climbingModule;
        }
    }
    public RaycastChecker ClimbingDetector
    {
        get
        {
            return climbingDetector;
        }
    }
    public MegaProjectileLauncher ArrowLauncher
    {
        get
        {
            return arrowLauncher;
        }
    }
    public float BowDrawDuration
    {
        get
        {
            return bowDrawDuration;
        }
    }
    public float RollDuration
    {
        get
        {
            return rollDuration;
        }
    }
    public float RollDeceleration
    {
        get
        {
            return rollDeceleration;
        }
    }
    public float RollSpeed
    {
        get
        {
            return rollSpeed;
        }
    }
    public float LandingDuration
    {
        get
        {
            return landingDuration;
        }
    }
    public float DodgeDuration
    {
        get
        {
            return dodgeDuration;
        }
    }
    public float DodgeSpeed
    {
        get
        {
            return dodgeSpeed;
        }
    }
    public float DodgeDeceleration
    {
        get
        {
            return dodgeDeceleration;
        }
    }
    public float ClimbingStartDuration
    {
        get
        {
            return climbingStartDuration;
        }
    }

    protected override void StartInitialize()
    {
    }

    protected override void EndUpdate()
    {
        if (recentStandardAttacks > 0 && !(CurrentState is AttackState) && TimeInState > standardAttackChainTime)
        {
            recentStandardAttacks = 0; //forget about chaining if we haven't been attacking for a long enough period
        }
    }

    protected override string GetInitialStateName()
    {
        return IDLE_STATE;
    }

    protected override Dictionary<string, PlayerState> GetStateDictionary()
    {
        Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>();

        IdleState idleState = new IdleState();
        AttackState attackState = new AttackState();
        AttackState attackState2 = new AttackState();
        JumpState jumpState = new JumpState();
        FallingState fallingState = new FallingState();
        MovingState movingState = new MovingState();
        RollState rollState = new RollState();
        DodgeState dodgeState = new DodgeState();
        DeathState deathState = new DeathState();
        ClimbingState climbingState = new ClimbingState();
        ClimbingReachTopState reachTopState = new ClimbingReachTopState();
        ClimbingStartState climbingStartState = new ClimbingStartState();
        LandingState landingState = new LandingState();
        LootState lootState = new LootState();
        BowDrawState bowDrawState = new BowDrawState();
        BowHoldState bowHoldState = new BowHoldState();
        SwordSpinState swordSpinState = new SwordSpinState();
        PlayerUpSlashState swordUpSlashState = new PlayerUpSlashState();
        PlayerDownSlashState swordDownSlashState = new PlayerDownSlashState();

        idleState.Initialize(this);
        attackState.Initialize(this);
        attackState2.Initialize(this);
        jumpState.Initialize(this);
        fallingState.Initialize(this);
        movingState.Initialize(this);
        rollState.Initialize(this);
        dodgeState.Initialize(this);
        deathState.Initialize(this);
        climbingState.Initialize(this);
        reachTopState.Initialize(this);
        climbingStartState.Initialize(this);
        landingState.Initialize(this);
        lootState.Initialize(this);
        bowDrawState.Initialize(this);
        bowHoldState.Initialize(this);
        swordSpinState.Initialize(this);
        swordUpSlashState.Initialize(this);
        swordDownSlashState.Initialize(this);

        dodgeState.OnEnter += DodgeState_OnEnter;
        climbingState.OnEnter += ClimbingState_OnEnter;
        climbingState.OnExit += ClimbingState_OnExit;

        states.Add(IDLE_STATE, idleState);
        states.Add(MOVING_STATE, movingState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(ATTACK2_STATE, attackState2);
        states.Add(JUMPING_STATE, jumpState);
        states.Add(FALLING_STATE, fallingState);
        states.Add(ROLL_STATE, rollState);
        states.Add(DODGING_STATE, dodgeState);
        states.Add(DYING_STATE, deathState);
        states.Add(CLIMBING_STATE, climbingState);
        states.Add(CLIMBING_REACH_TOP_STATE, reachTopState);
        states.Add(CLIMBING_START_STATE, climbingStartState);
        states.Add(LANDING_STATE, landingState);
        states.Add(LOOT_STATE, lootState);
        states.Add(BOW_DRAW_STATE, bowDrawState);
        states.Add(BOW_HOLD_STATE, bowHoldState);
        states.Add(SWORD_SPIN_STATE, swordSpinState);
        states.Add(SWORD_UP_SLASH_STATE, swordUpSlashState);
        states.Add(SWORD_DOWN_SLASH_STATE, swordDownSlashState);
        return states;
    }

    public void IncrementRecentStandardAttacks()
    {
        recentStandardAttacks++;
    }

    private void ClimbingState_OnEnter()
    {
        OnClimbingStart?.Invoke();
    }

    private void ClimbingState_OnExit()
    {
        OnClimbingStop?.Invoke();
    }

    private void DodgeState_OnEnter()
    {
        OnDodge?.Invoke();
    }

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }

    //shared between the default state and any state that should have the same transitions as it
    //currently unused
    public StateTransition[] GetDefaultTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(FALLING_STATE, () => !character.IsGrounded()),
            new StateTransition(ATTACK_STATE, () => trigger == ATTACK_STATE && recentStandardAttacks % 2 == 0),
            new StateTransition(ATTACK2_STATE, () => trigger == ATTACK_STATE && recentStandardAttacks % 2 != 0),
            new StateTransition(JUMPING_STATE, () => trigger == JUMPING_STATE),
            new StateTransition(ROLL_STATE, ToRollState),
            new StateTransition(DODGING_STATE, ToDodgeState),
            new StateTransition(CLIMBING_START_STATE, () => trigger == CLIMBING_START_STATE),
            new StateTransition(LOOT_STATE, () => trigger == LOOT_STATE),
            new StateTransition(BOW_DRAW_STATE, () => trigger == BOW_DRAW_STATE),
        };
    }

    private bool ToDodgeState()
    {
        return trigger == DODGE_TRIGGER && character.velocity.magnitude > 0 && defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.Strafe;
    }

    private bool ToRollState()
    {
        return trigger == DODGE_TRIGGER && character.velocity.magnitude > 0 && defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly;
    }

    public float CalculateDodgeMoveSpeed(float time)
    {
        return dodgeDeceleration * time + dodgeSpeed;
    }
}
