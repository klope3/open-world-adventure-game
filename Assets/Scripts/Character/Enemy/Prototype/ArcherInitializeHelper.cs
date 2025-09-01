using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInitializeHelper : MonoBehaviour
{
    [SerializeField] private MegaProjectileLauncher launcher;

    public void Initialize(GameObjectPool projectilePool)
    {
        launcher.pool = projectilePool;
    }
}
