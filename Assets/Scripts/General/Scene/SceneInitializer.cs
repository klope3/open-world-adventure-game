using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private PlayerDataPersister playerDataPersister;
    [SerializeField] private HealthHandler playerHealth;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private MoneyDisplay moneyDisplay;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private NonPlayerCharacterManager npcManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private PlayerInitialPositioner playerInitialPositioner;
    [SerializeField] private DunGen.RuntimeDungeon dungeonGenerator;
    [SerializeField] private DungeonFinalizer dungeonFinalizer;
    [SerializeField] private GameClock gameClock;
    [SerializeField] private CastleAttackScheduler castleAttackScheduler;

    private void Awake()
    {
        InputActionsProvider.UnlockPrimaryAxis();
        playerDataPersister.Initialize();
        playerHealth.Initialize(PersistentGameData.playerHealth);
        moneyHandler.Initialize(PersistentGameData.playerMoney);
        moneyDisplay.Initialize();
        gameClock.Initialize();
        castleAttackScheduler.Initialize();

        playerAnimation.Initialize();
        playerStateManager.Initialize();

        PooledObjectInitializer[] pooledObjectInitializers = FindObjectsOfType<PooledObjectInitializer>();
        foreach (PooledObjectInitializer i in pooledObjectInitializers)
        {
            i.Initialize();
        }

        GameObjectPool[] pools = FindObjectsOfType<GameObjectPool>();
        foreach (GameObjectPool p in pools)
        {
            p.Initialize();
        }

        npcManager.Initialize();
        gameStateManager.Initialize();
        playerInitialPositioner.PositionPlayer();
        if (dungeonFinalizer) dungeonFinalizer.Initialize();
        if (dungeonGenerator) dungeonGenerator.Generate();
    }
}
