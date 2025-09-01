using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInitializer : PooledObjectInitializer
{
    [SerializeField] private GameObjectPool archerPool;
    [SerializeField] private GameObjectPool archerProjectilePool;

    public override void Initialize()
    {
        archerPool.OnObjectInstantiated += GameObjectPool_OnObjectInstantiated;
    }

    private void OnDisable()
    {
        archerPool.OnObjectInstantiated -= GameObjectPool_OnObjectInstantiated;
    }

    private void GameObjectPool_OnObjectInstantiated(GameObject gameObject)
    {
        ArcherInitializeHelper helper = gameObject.GetComponent<ArcherInitializeHelper>();
        if (helper != null) helper.Initialize(archerProjectilePool);
    }
}
