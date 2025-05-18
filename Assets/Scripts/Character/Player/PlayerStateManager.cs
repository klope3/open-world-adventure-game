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
    [SerializeField] private CameraController cameraController;
    [SerializeField] private InputActionsEvents inputActionsEvents;
    [SerializeField] private TargetingHandler targetingHandler;
    [SerializeField] private PlayerClimbingDetector climbingDetector;
    [SerializeField] private PlayerClimbingModule climbingModule;
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

    public static readonly string DEFAULT_STATE = "Moving";
    public static readonly string ROLL_STATE = "Rolling";
    public static readonly string ATTACK_STATE = "Attacking";
    public static readonly string ATTACK2_STATE = "Attacking2";
    public static readonly string FALLING_STATE = "Falling";
    public static readonly string DODGING_STATE = "Dodging";
    public static readonly string JUMPING_STATE = "Jumping";
    public static readonly string CLIMBING_STATE = "Climbing";
    public static readonly string DYING_STATE = "Dying";
    public static readonly string LANDING_STATE = "Landing";
    public static readonly string INTERACT_TRIGGER = "Interact";

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
    public PlayerClimbingDetector ClimbingDetector
    {
        get
        {
            return climbingDetector;
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

    protected override void StartAwake()
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
        return DEFAULT_STATE;
    }

    protected override Dictionary<string, PlayerState> GetStateDictionary()
    {
        Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>();

        AttackState attackState = new AttackState();
        AttackState attackState2 = new AttackState();
        JumpState jumpState = new JumpState();
        FallingState fallingState = new FallingState();
        DefaultState movingState = new DefaultState();
        RollState rollState = new RollState();
        DodgeState dodgeState = new DodgeState();
        DeathState deathState = new DeathState();
        ClimbingState climbingState = new ClimbingState();
        LandingState landingState = new LandingState();

        attackState.Initialize(this);
        attackState2.Initialize(this);
        jumpState.Initialize(this);
        fallingState.Initialize(this);
        movingState.Initialize(this);
        rollState.Initialize(this);
        dodgeState.Initialize(this);
        deathState.Initialize(this);
        climbingState.Initialize(this);
        landingState.Initialize(this);

        dodgeState.OnEnter += DodgeState_OnEnter;
        climbingState.OnEnter += ClimbingState_OnEnter;
        climbingState.OnExit += ClimbingState_OnExit;

        states.Add(DEFAULT_STATE, movingState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(ATTACK2_STATE, attackState2);
        states.Add(JUMPING_STATE, jumpState);
        states.Add(FALLING_STATE, fallingState);
        states.Add(ROLL_STATE, rollState);
        states.Add(DODGING_STATE, dodgeState);
        states.Add(DYING_STATE, deathState);
        states.Add(CLIMBING_STATE, climbingState);
        states.Add(LANDING_STATE, landingState);
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
    public StateTransition[] GetDefaultTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(FALLING_STATE, () => !character.IsGrounded() && character.velocity.y < 0),
            new StateTransition(ATTACK_STATE, () => trigger == ATTACK_STATE && recentStandardAttacks % 2 == 0),
            new StateTransition(ATTACK2_STATE, () => trigger == ATTACK_STATE && recentStandardAttacks % 2 != 0),
            new StateTransition(JUMPING_STATE, () => trigger == JUMPING_STATE),
            new StateTransition(ROLL_STATE, ToRollState),
            new StateTransition(CLIMBING_STATE, () => trigger == INTERACT_TRIGGER && climbingDetector.CheckClimbable()),
        };
    }

    private bool ToRollState()
    {
        return trigger == ROLL_STATE && character.velocity.magnitude > 0 && defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly;
    }
}
