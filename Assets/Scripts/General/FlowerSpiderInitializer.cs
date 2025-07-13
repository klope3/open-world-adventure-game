using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpiderInitializer : PooledObjectInitializer
{
    [SerializeField] private GameObjectPool slimeBallPool;
    [SerializeField] private GameObjectPool flowerSpiderPool;
    [SerializeField] private GameObject playerObject;

    public override void Initialize()
    {
        flowerSpiderPool.OnObjectInstantiated += GameObjectPool_OnObjectInstantiated;
    }

    private void OnDisable()
    {
        flowerSpiderPool.OnObjectInstantiated -= GameObjectPool_OnObjectInstantiated;
    }

    private void GameObjectPool_OnObjectInstantiated(GameObject gameObject)
    {
        EnemyStateManager stateManager = gameObject.GetComponent<EnemyStateManager>();
        stateManager.SetPlayer(playerObject);

        FlowerSpiderAttack attack = gameObject.GetComponent<FlowerSpiderAttack>();
        attack.SetLauncherPools(slimeBallPool);
    }
}
