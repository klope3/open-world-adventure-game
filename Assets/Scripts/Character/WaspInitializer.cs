using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspInitializer : MonoBehaviour
{
    [SerializeField] private LookAtTransform launcherAimer;
    [SerializeField] private MegaProjectileLauncher launcher;

    public void Initialize(GameObjectPool projectilePool)
    {
        launcher.pool = projectilePool;
        launcherAimer.lookAtTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
