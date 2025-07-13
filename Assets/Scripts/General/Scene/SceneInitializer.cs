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

    private void Awake()
    {
        InputActionsProvider.UnlockPrimaryAxis();
        playerDataPersister.Initialize();
        playerHealth.Initialize(PersistentGameData.playerHealth);
        moneyHandler.Initialize(PersistentGameData.playerMoney);
        moneyDisplay.Initialize();

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
    }
}
