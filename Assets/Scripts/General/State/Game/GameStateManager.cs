using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateManager<GameState>
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2.Character playerCharacter;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private NonPlayerCharacterManager npcManager;
    [SerializeField] private GameClock gameClock;
    [field: SerializeField] public bool AllowPauseMenu;

    public PlayerStateManager PlayerStateManager
    {
        get
        {
            return playerStateManager;
        }
    }
    public ECM2.Character PlayerCharacter
    {
        get
        {
            return playerCharacter;
        }
    }
    public Animator PlayerAnimator
    {
        get
        {
            return playerAnimator;
        }
    }
    public NonPlayerCharacterManager NPCManager
    {
        get
        {
            return npcManager;
        }
    }
    public GameClock GameClock
    {
        get
        {
            return gameClock;
        }
    }

    public static readonly string DEFAULT_STATE = "Default";
    public static readonly string LOOT_STATE = "Loot";
    public static readonly string PAUSE_STATE = "Pause";

    protected override Dictionary<string, GameState> GetStateDictionary()
    {
        Dictionary<string, GameState> states = new Dictionary<string, GameState>();

        GameDefaultState defaultState = new GameDefaultState();
        GameLootState lootState = new GameLootState();
        GamePauseState pauseState = new GamePauseState();

        defaultState.Initialize(this);
        lootState.Initialize(this);
        pauseState.Initialize(this);

        states.Add(DEFAULT_STATE, defaultState);
        states.Add(LOOT_STATE, lootState);
        states.Add(PAUSE_STATE, pauseState);

        return states;
    }

    protected override void StartInitialize()
    {
    }

    protected override void EndUpdate()
    {
    }

    protected override string GetInitialStateName()
    {
        return DEFAULT_STATE;
    }
}
