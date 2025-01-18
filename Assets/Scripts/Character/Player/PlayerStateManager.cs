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
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float meleeZoneActiveTime;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private float stopMovementTime;
    public System.Action OnAttack;
    public System.Action OnLeftGround;

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

        idleState.Initialize(this, character, characterAdapter, interactionZone, cameraController);
        attackState.Initialize(this, character, characterAdapter, meleeZone, 1 / attacksPerSecond);
        jumpState.Initialize(this, character, characterAdapter);
        fallingState.Initialize(this, character, characterAdapter);
        dialogueState.Initialize(this, character, characterAdapter, dialogueManager);

        attackState.OnEnter += AttackState_OnEnter;
        fallingState.OnEnter += FallingState_OnEnter;

        states.Add("Idle", idleState);
        states.Add("Attack", attackState);
        states.Add("Jump", jumpState);
        states.Add("Falling", fallingState);
        states.Add("Dialogue", dialogueState);
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

    private void CharacterAdapter_LeftGround()
    {
        SwitchState("Falling");
    }

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }
}
