using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Receives a projectile and damages the health handler accordingly.
//In the future this would probably decide how much damage do apply based on various projectile characteristics such as projectile type, damage amount, etc.
public class ProjectileHealthDamager : MegaProjectileImpactable
{
    [SerializeField] private HealthHandler healthHandler;

    public override void ReceiveProjectile(MegaProjectile projectile, RaycastHit hitInfo)
    {
        base.ReceiveProjectile(projectile, hitInfo);
        healthHandler.AddHealth(-1, hitInfo.point);
    }
}
