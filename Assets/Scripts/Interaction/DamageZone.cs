using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

[RequireComponent(typeof(Collider))]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private float force;
    [SerializeField] private Transform forceOrigin;

    private void OnTriggerStay(Collider other)
    {
        HealthHandler health = other.GetComponent<HealthHandler>();
        if (health != null)
        {
            Vector3 point = other.ClosestPoint(transform.position);
            health.AddHealth(-1 * amount, point);
        }

        Character character = other.GetComponent<Character>();
        if (character != null && force > 0)
        {
            character.AddExplosionForce(force, forceOrigin.position, 1000, 0, ForceMode.Impulse);
        }
    }
}
