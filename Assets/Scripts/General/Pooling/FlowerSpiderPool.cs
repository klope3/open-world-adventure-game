using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpiderPool : GameObjectPool
{
    [SerializeField] private GameObjectPool slimeBallPool;

    protected override GameObject InstantiateObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        FlowerSpiderAttack attack = go.GetComponent<FlowerSpiderAttack>();
        attack.SetLauncherPools(slimeBallPool);
        return go;
    }
}
