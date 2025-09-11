using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class PlayerStateManager : StateManager<PlayerState>
{
    [field: SerializeField] public PlayerDefaultMovementModule DefaultMovementModule { get; private set; }
    [field: SerializeField] public PlayerControlDataSO PlayerControlDataSO { get; private set;}
    [field: SerializeField] public DamageZone SwordDamageZone { get; private set; }
    [field: SerializeField] public LedgeChecker LedgeChecker { get; private set; }
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public Collider ShieldCollider { get; private set; }
    [field: SerializeField] public PlayerClimbingModule ClimbingModule { get; private set; }
    [field: SerializeField] public RaycastChecker ClimbingDetector { get; private set; }
    [field: SerializeField] public MegaProjectileLauncher ArrowLauncher { get; private set; }
    [HideInInspector] public float cachedPlayerSpeed; //useful for when player speed needs to be reset to a previous value after multiple state transitions
    [HideInInspector] public float cachedPlayerAcceleration;
    //private int recentStandardAttacks; //increments while chaining attacks; resets to 0 when standardAttackChainTime elapses

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
    public static readonly string LEDGE_HANG_STATE = "Ledge Hang";
    public static readonly string LEDGE_JUMP_UP_STATE = "Ledge Jump Up";
    public static readonly string SHIELD_HOLD_STATE = "shield hold";


    protected override void StartInitialize()
    {
    }

    protected override void EndUpdate()
    {
        //if (recentStandardAttacks > 0 && !(CurrentState is AttackState) && TimeInState > PlayerControlDataSO.StandardAttackChainTime)
        //{
        //    recentStandardAttacks = 0; //forget about chaining if we haven't been attacking for a long enough period
        //}
    }

    protected override string GetInitialStateName()
    {
        return IDLE_STATE;
    }

    protected override Dictionary<string, PlayerState> GetStateDictionary()
    {
        Dictionary<string, PlayerState> states = new Dictionary<string, PlayerState>()
        {
            { IDLE_STATE, new IdleState() },
            { ATTACK_STATE, new AttackState() },
            { ATTACK2_STATE, new AttackState() },
            { JUMPING_STATE, new JumpState() },
            { FALLING_STATE, new FallingState() },
            { MOVING_STATE, new MovingState() },
            { ROLL_STATE, new RollState() },
            { DODGING_STATE, new DodgeState() },
            { DYING_STATE, new DeathState() },
            { CLIMBING_STATE, new ClimbingState() },
            { CLIMBING_REACH_TOP_STATE, new ClimbingReachTopState() },
            { CLIMBING_START_STATE, new ClimbingStartState() },
            { LANDING_STATE, new LandingState() },
            { LOOT_STATE, new LootState() },
            { BOW_DRAW_STATE, new BowDrawState() },
            { BOW_HOLD_STATE, new BowHoldState() },
            { SWORD_SPIN_STATE, new SwordSpinState() },
            { SWORD_UP_SLASH_STATE, new PlayerUpSlashState() },
            { SWORD_DOWN_SLASH_STATE, new PlayerDownSlashState() },
            { LEDGE_HANG_STATE, new LedgeHangState() },
            { LEDGE_JUMP_UP_STATE, new LedgeHangJumpUpState() },
            { SHIELD_HOLD_STATE, new ShieldHoldState() },
        };

        foreach (KeyValuePair<string, PlayerState> state in states)
        {
            state.Value.Initialize(this);
        }

        return states;
    }

    //public void IncrementRecentStandardAttacks()
    //{
    //    recentStandardAttacks++;
    //}

    public bool IsInState(PlayerState state)
    {
        return CurrentState == state;
    }

    public float CalculateDodgeMoveSpeed(float time)
    {
        return PlayerControlDataSO.DodgeDeceleration * time + PlayerControlDataSO.DodgeSpeed;
    }
}
