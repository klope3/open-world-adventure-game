using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using UnityEngine.Events;

public class PlayerStateManager : StateManager<PlayerState>
{
    [SerializeField] private ECM2CharacterAdapter characterAdapter;
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
    private MovementType movementType;
    private int recentStandardAttacks; //increments while chaining attacks; resets to 0 when standardAttackChainTime elapses
    public System.Action OnDefaultState;
    //public System.Action OnAttack;
    public System.Action OnAttack2;
    public System.Action OnLeftGround;
    //public System.Action OnRoll;
    public System.Action OnDodge;
    public System.Action OnClimbingStart;
    public System.Action OnClimbingStop;
    //public System.Action OnFallingStart;
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

    public enum MovementType
    {
        ForwardOnly, //only animate between idle, walk forward, and run forward (assumes character's body is always facing movement direction)
        Strafe //forward, backward, strafe, and in-between animations (for when character's body doesn't necessarily face movement direction)
    }

    public ECM2CharacterAdapter CharacterAdapter
    {
        get
        {
            return characterAdapter;
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
    public MovementType CurrentMovementType
    {
        get
        {
            return movementType;
        }
    }

    protected override void StartAwake()
    {
        InputActionsProvider.OnZTargetStarted += ZTarget_started;
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

        //IdleState idleState = new IdleState();
        AttackState attackState = new AttackState();
        AttackState attackState2 = new AttackState();
        JumpState jumpState = new JumpState();
        FallingState fallingState = new FallingState();
        //DialogueState dialogueState = new DialogueState();
        MovingState movingState = new MovingState();
        RollState rollState = new RollState();
        DodgeState dodgeState = new DodgeState();
        DeathState deathState = new DeathState();
        ClimbingState climbingState = new ClimbingState();
        LandingState landingState = new LandingState();

        //idleState.Initialize(this, character, characterAdapter, interactionZone, targetingHandler);
        //attackState.Initialize(this, character, characterAdapter, standardAttackDuration);
        //attackState2.Initialize(this, character, characterAdapter, standardAttackDuration);
        //jumpState.Initialize(this, character, characterAdapter);
        //fallingState.Initialize(this, character, characterAdapter);
        //dialogueState.Initialize(this, character, characterAdapter, dialogueManager, dialogueBox, inputActionsEvents);
        //movingState.Initialize(this, character, characterAdapter, interactionZone, targetingHandler, cameraController, climbingDetector);
        //rollState.Initialize(this, character, characterAdapter, rollSpeed, rollDuration, rollDeceleration);
        //dodgeState.Initialize(this, character, characterAdapter, dodgeSpeed, dodgeDuration, dodgeDeceleration);
        //deathState.Initialize(this, character, characterAdapter);
        //climbingState.Initialize(this, character, characterAdapter, climbingModule);
        //landingState.Initialize(this, character, characterAdapter, landingDuration);

        attackState.Initialize_NEW(this);
        attackState2.Initialize_NEW(this);
        jumpState.Initialize_NEW(this);
        fallingState.Initialize_NEW(this);
        //dialogueState.Initialize_NEW(this);
        movingState.Initialize_NEW(this);
        rollState.Initialize_NEW(this);
        dodgeState.Initialize_NEW(this);
        deathState.Initialize_NEW(this);
        climbingState.Initialize_NEW(this);
        landingState.Initialize_NEW(this);

        //idleState.PostInitialize();
        //attackState.PostInitialize();
        //attackState2.PostInitialize();
        //jumpState.PostInitialize();
        //fallingState.PostInitialize();
        //dialogueState.PostInitialize();
        //movingState.PostInitialize();
        //rollState.PostInitialize();
        //dodgeState.PostInitialize();
        //deathState.PostInitialize();
        //climbingState.PostInitialize();
        //landingState.PostInitialize();

        movingState.OnEnter += DefaultState_OnEnter;
        //attackState.OnEnter += AttackState_OnEnter;
        //attackState.OnExit += AttackState_OnExit;
        //attackState2.OnEnter += AttackState2_OnEnter;
        //attackState2.OnExit += AttackState2_OnExit;
        //fallingState.OnEnter += FallingState_OnEnter;
        //rollState.OnEnter += RollState_OnEnter;
        dodgeState.OnEnter += DodgeState_OnEnter;
        climbingState.OnEnter += ClimbingState_OnEnter;
        climbingState.OnExit += ClimbingState_OnExit;
        //landingState.OnEnter += LandingState_OnEnter;

        //states.Add("Idle", idleState);
        states.Add(DEFAULT_STATE, movingState);
        states.Add(ATTACK_STATE, attackState);
        states.Add(ATTACK2_STATE, attackState2);
        states.Add(JUMPING_STATE, jumpState);
        states.Add(FALLING_STATE, fallingState);
        //states.Add("Dialogue", dialogueState);
        states.Add(ROLL_STATE, rollState);
        states.Add(DODGING_STATE, dodgeState);
        states.Add(DYING_STATE, deathState);
        states.Add(CLIMBING_STATE, climbingState);
        states.Add(LANDING_STATE, landingState);
        return states;
    }

    private bool CheckFalling()
    {
        return !character.IsGrounded() && character.velocity.y < 0;
    }

    public void IncrementRecentStandardAttacks()
    {
        recentStandardAttacks++;
    }

    private void ZTarget_started()
    {
        Debug.Log("Target");
    }

    private void LandingState_OnEnter()
    {
        OnLand?.Invoke();
        OnLanded?.Invoke();
    }

    private void DefaultState_OnEnter()
    {
        OnDefaultState?.Invoke();
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

    //private void AttackState_OnEnter()
    //{
    //    OnAttack?.Invoke();
    //    OnAnyAttackStart?.Invoke();
    //}
    //
    //private void AttackState_OnExit()
    //{
    //    OnAnyAttackEnd?.Invoke();
    //}
    //
    //private void AttackState2_OnEnter()
    //{
    //    OnAttack2?.Invoke();
    //    OnAnyAttackStart?.Invoke();
    //}
    //
    //private void AttackState2_OnExit()
    //{
    //    OnAnyAttackEnd?.Invoke();
    //}

    //private void FallingState_OnEnter()
    //{
    //    OnLeftGround?.Invoke();
    //}

    //private void RollState_OnEnter()
    //{
    //    OnRoll?.Invoke();
    //}

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }

    //attacking has special chaining behavior so implement custom logic for it
    public void TryDoAttack()
    {
        if (CurrentState is AttackState) return;
        if (recentStandardAttacks % 2 != 0) SwitchState(ATTACK2_STATE);
        else SwitchState(ATTACK_STATE);

        recentStandardAttacks++;
    }

    //shared between the default state and any state that should have the same transitions as it
    public StateTransition[] GetDefaultTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(FALLING_STATE, () => !character.IsGrounded() && character.velocity.y < 0),
            new StateTransition(ATTACK_STATE, ToAttack1),
            new StateTransition(ATTACK2_STATE, ToAttack2),
            new StateTransition(JUMPING_STATE, () => trigger == JUMPING_STATE),
            new StateTransition(ROLL_STATE, () => trigger == ROLL_STATE && character.velocity.magnitude > 0 && movementType == MovementType.ForwardOnly),
        };
    }

    private bool ToAttack1()
    {
        return trigger == ATTACK_STATE && recentStandardAttacks % 2 == 0;
    }

    private bool ToAttack2()
    {
        return trigger == ATTACK_STATE && recentStandardAttacks % 2 != 0;
    }
}
