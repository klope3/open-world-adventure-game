using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberInitializeHelper : MonoBehaviour, IPoolInitializable
{
    [SerializeField] private RigidbodyLauncher rigidbodyLauncher;

    public void Initialize(GameObjectPool pool)
    {
        rigidbodyLauncher.SetGameObjectPool(pool);
    }
}
