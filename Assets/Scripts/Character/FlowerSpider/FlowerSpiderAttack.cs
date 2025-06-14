using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpiderAttack : MonoBehaviour
{
    [SerializeField] private RigidbodyLauncher[] launchers;

    public void Attack()
    {
        foreach (RigidbodyLauncher launcher in launchers)
        {
            launcher.Launch();
        }
    }

    public void SetLauncherPools(GameObjectPool poolToSet)
    {
        foreach (RigidbodyLauncher launcher in launchers)
        {
            launcher.SetGameObjectPool(poolToSet);
        }
    }
}
