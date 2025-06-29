using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MegaProjectileImpactable : MonoBehaviour
{
    public UnityEvent OnImpact;
    public delegate void ImpactEvent(MegaProjectile projectile);
    public event ImpactEvent OnProjectileImpact;

    public virtual void ReceiveProjectile(MegaProjectile projectile, RaycastHit hitInfo)
    {
        OnImpact?.Invoke();
        OnProjectileImpact?.Invoke(projectile);
    }
}
