using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspInitializer : PooledObjectInitializer
{
    [SerializeField] private GameObjectPool waspPool;
    [SerializeField] private GameObjectPool waspProjectilePool;
    [SerializeField] private GameObject playerObject;

    public override void Initialize()
    {
        waspPool.OnObjectInstantiated += GameObjectPool_OnObjectInstantiated;
    }

    private void GameObjectPool_OnObjectInstantiated(GameObject gameObject)
    {
        EnemyStateManager stateManager = gameObject.GetComponent<EnemyStateManager>();
        stateManager.SetPlayer(playerObject);

        WaspInitializeHelper helper = gameObject.GetComponent<WaspInitializeHelper>();
        helper.Initialize(waspProjectilePool);
    }
}
