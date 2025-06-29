using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private MegaProjectileLauncher arrowLauncher;

    public void Initialize(GameObjectPool arrowPool)
    {
        arrowLauncher.pool = arrowPool;
    }
}
