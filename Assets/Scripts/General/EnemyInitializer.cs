using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : PooledObjectInitializer
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObjectPool pool;

    public override void Initialize()
    {
        pool.OnObjectInstantiated += GameObjectPool_OnObjectInstantiated;
    }

    private void GameObjectPool_OnObjectInstantiated(GameObject gameObject)
    {
        EnemyStateManager stateManager = gameObject.GetComponent<EnemyStateManager>();
        stateManager.SetPlayer(playerObject);
    }
}
