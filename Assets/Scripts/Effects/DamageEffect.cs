using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private ParticleSystem effect;

    private void OnEnable()
    {
        healthHandler.OnDamaged += HealthHandler_OnDamaged;
    }

    private void OnDisable()
    {
        healthHandler.OnDamaged -= HealthHandler_OnDamaged;
    }

    private void HealthHandler_OnDamaged(Vector3 position)
    {
        effect.transform.position = position;
        effect.Play();
    }
}
