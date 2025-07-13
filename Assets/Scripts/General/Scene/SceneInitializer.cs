using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private NonPlayerCharacterManager npcManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private PlayerInitialPositioner playerInitialPositioner;

    private void Awake()
    {
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
