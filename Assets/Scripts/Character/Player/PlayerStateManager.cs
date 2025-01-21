using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

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
    public System.Action OnAttack;
    public System.Action OnLeftGround;
    public System.Action OnRoll;

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
        MoveForwardState moveForwardState = new MoveForwardState();
        RollState rollState = new RollState();

        idleState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        attackState.Initialize(this, character, characterAdapter, meleeZone, 1 / attacksPerSecond);
        jumpState.Initialize(this, character, characterAdapter);
        fallingState.Initialize(this, character, characterAdapter);
        dialogueState.Initialize(this, character, characterAdapter, dialogueManager, dialogueBox, inputActionsEvents);
        moveForwardState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        rollState.Initialize(this, character, characterAdapter, rollSpeed, rollDuration, rollDeceleration);

        idleState.PostInitialize();
        attackState.PostInitialize();
        jumpState.PostInitialize();
        fallingState.PostInitialize();
        dialogueState.PostInitialize();
        moveForwardState.PostInitialize();
        rollState.PostInitialize();

        attackState.OnEnter += AttackState_OnEnter;
        fallingState.OnEnter += FallingState_OnEnter;
        rollState.OnEnter += RollState_OnEnter;

        states.Add("Idle", idleState);
        states.Add("Attack", attackState);
        states.Add("Jump", jumpState);
        states.Add("Falling", fallingState);
        states.Add("Dialogue", dialogueState);
        states.Add("MoveForward", moveForwardState);
        states.Add("Roll", rollState);
        return states;
    }

    private void AttackState_OnEnter()
    {
        OnAttack?.Invoke();
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
