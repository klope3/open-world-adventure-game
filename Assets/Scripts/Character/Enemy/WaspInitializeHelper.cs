using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspInitializeHelper : MonoBehaviour
{
    [SerializeField] private LookAtPosition launcherAimer;
    [SerializeField] private MegaProjectileLauncher launcher;

    public void Initialize(GameObjectPool projectilePool)
    {
        launcher.pool = projectilePool;
        launcherAimer.lookAtTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
