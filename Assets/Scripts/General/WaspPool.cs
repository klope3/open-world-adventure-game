using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspPool : GameObjectPool
{
    [SerializeField] private GameObjectPool projectilePool;

    protected override GameObject InstantiateObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        WaspInitializeHelper initializer = go.GetComponent<WaspInitializeHelper>();
        initializer.Initialize(projectilePool);

        return go;
    }
}
