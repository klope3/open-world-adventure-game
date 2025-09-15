using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for pooled objects that themselves need references to a pool, initializes each pooled object with the needed reference.
public class PooledObjectPoolInitializer : PooledObjectInitializer
{
    [SerializeField] private GameObjectPool targetPool;
    [SerializeField, Tooltip("This pool reference will be provided to each object pooled in targetPool.")] private GameObjectPool poolToAssign;

    public override void Initialize()
    {
        targetPool.OnObjectInstantiated += GameObjectPool_OnObjectInstantiated;
    }

    private void OnDisable()
    {
        targetPool.OnObjectInstantiated -= GameObjectPool_OnObjectInstantiated;
    }

    private void GameObjectPool_OnObjectInstantiated(GameObject gameObject)
    {
        IPoolInitializable poolInitializable = gameObject.GetComponent<IPoolInitializable>();
        if (poolInitializable != null) poolInitializable.Initialize(poolToAssign);
    }
}
