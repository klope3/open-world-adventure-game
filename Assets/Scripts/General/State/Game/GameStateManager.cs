using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : StateManager<GameState>
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2.Character playerCharacter;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private NonPlayerCharacterManager npcManager;
    [SerializeField] private GameClock gameClock;

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
    public CameraController CameraController
    {
        get
        {
            return cameraController;
        }
    }

    public static readonly string DEFAULT_STATE = "Default";
    public static readonly string LOOT_STATE = "Loot";

    protected override Dictionary<string, GameState> GetStateDictionary()
    {
        Dictionary<string, GameState> states = new Dictionary<string, GameState>();

        GameDefaultState defaultState = new GameDefaultState();
        GameLootState lootState = new GameLootState();

        defaultState.Initialize(this);
        lootState.Initialize(this);

        states.Add(DEFAULT_STATE, defaultState);
        states.Add(LOOT_STATE, lootState);

        return states;
    }

    protected override void StartAwake()
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
