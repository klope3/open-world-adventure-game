using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MegaProjectileAmmoModule : MonoBehaviour
{
    [SerializeField, Min(0)] private int maxAmmo;
    [SerializeField, Min(0)] private int startingAmmo;
    [ShowInInspector, DisplayAsString] private int ammo;
    
    public int Ammo { get { return ammo; } }

    private void Awake()
    {
        ammo = startingAmmo;
    }

    private void OnValidate()
    {
        if (startingAmmo > maxAmmo)
            startingAmmo = maxAmmo;
    }

    public void Add(int amt)
    {
        ammo += amt;
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
    }
}
