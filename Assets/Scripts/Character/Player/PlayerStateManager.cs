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
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private float stopMovementTime;
    [SerializeField] private float rollDuration;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDeceleration;
    [SerializeField] private float dodgeDuration;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeDeceleration;
    [SerializeField] private float dodgeJumpForce;
    public System.Action OnAttack;
    public System.Action OnLeftGround;
    public System.Action OnRoll;
    public System.Action OnDodge;

    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

    protected override void StartAwake()
    {
        characterAdapter.LeftGround += CharacterAdapter_LeftGround;
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
        JumpState jumpState = new JumpState();
        FallingState fallingState = new FallingState();
        DialogueState dialogueState = new DialogueState();
        MovingState movingState = new MovingState();
        RollState rollState = new RollState();
        DodgeState dodgeState = new DodgeState();

        idleState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        attackState.Initialize(this, character, characterAdapter, meleeZone, 1 / attacksPerSecond);
        jumpState.Initialize(this, character, characterAdapter);
        fallingState.Initialize(this, character, characterAdapter);
        dialogueState.Initialize(this, character, characterAdapter, dialogueManager, dialogueBox, inputActionsEvents);
        movingState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        rollState.Initialize(this, character, characterAdapter, rollSpeed, rollDuration, rollDeceleration);
        dodgeState.Initialize(this, character, characterAdapter, dodgeSpeed, dodgeDuration, dodgeDeceleration);

        idleState.PostInitialize();
        attackState.PostInitialize();
        jumpState.PostInitialize();
        fallingState.PostInitialize();
        dialogueState.PostInitialize();
        movingState.PostInitialize();
        rollState.PostInitialize();
        dodgeState.PostInitialize();

        attackState.OnEnter += AttackState_OnEnter;
        attackState.OnExit += AttackState_OnExit;
        fallingState.OnEnter += FallingState_OnEnter;
        rollState.OnEnter += RollState_OnEnter;
        dodgeState.OnEnter += DodgeState_OnEnter;

        states.Add("Idle", idleState);
        states.Add("Attack", attackState);
        states.Add("Jump", jumpState);
        states.Add("Falling", fallingState);
        states.Add("Dialogue", dialogueState);
        states.Add("Moving", movingState);
        states.Add("Roll", rollState);
        states.Add("Dodge", dodgeState);
        return states;
    }

    private void DodgeState_OnEnter()
    {
        OnDodge?.Invoke();
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

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }
}
