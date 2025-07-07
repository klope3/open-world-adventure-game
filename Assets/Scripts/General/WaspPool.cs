using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspPool : GameObjectPool
{
    [SerializeField] private GameObjectPool projectilePool;

    protected override GameObject InstantiateObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        WaspInitializer initializer = go.GetComponent<WaspInitializer>();
        initializer.Initialize(projectilePool);

        return go;
    }
}
