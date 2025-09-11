using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [Sirenix.OdinInspector.InfoBox("Each of these fields are optional and can be assigned as needed based on what events need to be listened for.")]
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private BasicDamageReceiver damageReceiver;
    [SerializeField] private MegaProjectileImpactable impactable;

    private void OnEnable()
    {
        if (healthHandler != null) healthHandler.OnDamaged += Play;
        if (damageReceiver != null) damageReceiver.OnReceivedDamage += Play;
        if (impactable != null) impactable.OnProjectileImpact += Impactable_OnProjectileImpact;
    }

    private void Impactable_OnProjectileImpact(MegaProjectile projectile, RaycastHit hitInfo)
    {
        Play(hitInfo.point);
    }

    private void OnDisable()
    {
        if (healthHandler != null) healthHandler.OnDamaged -= Play;
        if (damageReceiver != null) damageReceiver.OnReceivedDamage -= Play;
        if (impactable != null) impactable.OnProjectileImpact -= Impactable_OnProjectileImpact;
    }

    private void Play(Vector3 position)
    {
        effect.transform.position = position;
        effect.Play();
    }
}
