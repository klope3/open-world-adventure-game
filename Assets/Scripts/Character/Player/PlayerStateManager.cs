using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using UnityEngine.Events;

public class PlayerStateManager : StateManager<PlayerState>
{
    [SerializeField] private Animator animator;
    [SerializeField] private ECM2CharacterAdapter characterAdapter;
    [SerializeField] private Character character;
    [SerializeField] private Collider meleeZone;
    [SerializeField] private InteractionZone interactionZone;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private InputActionsEvents inputActionsEvents;
    [SerializeField] private float meleeZoneActiveTime;
    //[SerializeField] private float attacksPerSecond;
    [SerializeField] private float standardAttackDuration;
    //[SerializeField, Tooltip("The player must press attack at least this long after the current attack started to chain to the next attack.")] 
    //private float standardAttackChainDelay;
    [SerializeField, Tooltip("The player must press attack at most this long after the previous attack state finished in order to chain the next attack.")]
    private float standardAttackChainTime;
    [SerializeField] private float stopMovementTime;
    [SerializeField] private float rollDuration;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDeceleration;
    [SerializeField] private float dodgeDuration;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeDeceleration;
    [SerializeField] private float dodgeJumpForce;
    private int recentStandardAttacks; //increments while chaining attacks; resets to 0 when standardAttackChainTime elapses
    public System.Action OnAttack;
    public System.Action OnAttack2;
    public System.Action OnLeftGround;
    public System.Action OnRoll;
    public System.Action OnDodge;

    public UnityEvent OnAnyAttackStart; 
    public UnityEvent OnAnyAttackEnd;
    public UnityEvent OnLanded;

    protected override void StartAwake()
    {
        characterAdapter.LeftGround += CharacterAdapter_LeftGround;
        character.Landed += Character_Landed;
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
        return "Idle";
    }

    protected override Dictionary<string, PlayerState> GetStateDictionary()
    {
        Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>();

        IdleState idleState = new IdleState();
        AttackState attackState = new AttackState();
        AttackState attackState2 = new AttackState();
        //Attack2State attack2State = new Attack2State();
        JumpState jumpState = new JumpState();
        FallingState fallingState = new FallingState();
        DialogueState dialogueState = new DialogueState();
        MovingState movingState = new MovingState();
        RollState rollState = new RollState();
        DodgeState dodgeState = new DodgeState();

        idleState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        attackState.Initialize(this, character, characterAdapter, standardAttackDuration);
        attackState2.Initialize(this, character, characterAdapter, standardAttackDuration);
        //attack2State.Initialize(this, character, characterAdapter, standardAttackDuration);
        jumpState.Initialize(this, character, characterAdapter);
        fallingState.Initialize(this, character, characterAdapter);
        dialogueState.Initialize(this, character, characterAdapter, dialogueManager, dialogueBox, inputActionsEvents);
        movingState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        rollState.Initialize(this, character, characterAdapter, rollSpeed, rollDuration, rollDeceleration);
        dodgeState.Initialize(this, character, characterAdapter, dodgeSpeed, dodgeDuration, dodgeDeceleration);

        idleState.PostInitialize();
        attackState.PostInitialize();
        attackState2.PostInitialize();
        //attack2State.PostInitialize();
        jumpState.PostInitialize();
        fallingState.PostInitialize();
        dialogueState.PostInitialize();
        movingState.PostInitialize();
        rollState.PostInitialize();
        dodgeState.PostInitialize();

        attackState.OnEnter += AttackState_OnEnter;
        attackState.OnExit += AttackState_OnExit;
        attackState2.OnEnter += AttackState2_OnEnter;
        attackState2.OnExit += AttackState2_OnExit;
        //attack2State.OnEnter += Attack2State_OnEnter;
        //attack2State.OnExit += Attack2State_OnExit;
        fallingState.OnEnter += FallingState_OnEnter;
        rollState.OnEnter += RollState_OnEnter;
        dodgeState.OnEnter += DodgeState_OnEnter;

        states.Add("Idle", idleState);
        states.Add("Attack", attackState);
        states.Add("Attack2", attackState2);
        //states.Add("Attack2", attack2State);
        states.Add("Jump", jumpState);
        states.Add("Falling", fallingState);
        states.Add("Dialogue", dialogueState);
        states.Add("Moving", movingState);
        states.Add("Roll", rollState);
        states.Add("Dodge", dodgeState);
        return states;
    }

    //attacking has special chaining behavior so implement custom logic for it
    public void TryDoAttack()
    {
        if (CurrentState is AttackState) return;
        if (recentStandardAttacks % 2 != 0) SwitchState("Attack2");
        else SwitchState("Attack");

        recentStandardAttacks++;
    }

    private void DodgeState_OnEnter()
    {
        OnDodge?.Invoke();
    }

    private void AttackState_OnEnter()
    {
        OnAttack?.Invoke();
        OnAnyAttackStart?.Invoke();
    }

    private void AttackState_OnExit()
    {
        OnAnyAttackEnd?.Invoke();
    }

    private void AttackState2_OnEnter()
    {
        OnAttack2?.Invoke();
        OnAnyAttackStart?.Invoke();
    }

    private void AttackState2_OnExit()
    {
        OnAnyAttackEnd?.Invoke();
    }

    private void FallingState_OnEnter()
    {
        OnLeftGround?.Invoke();
    }

    private void RollState_OnEnter()
    {
        OnRoll?.Invoke();
    }

    private void CharacterAdapter_LeftGround()
    {
        SwitchState("Falling");
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        OnLanded?.Invoke();
    }

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }
}
