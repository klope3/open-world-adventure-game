using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private int amount;

    private void OnTriggerStay(Collider other)
    {
        HealthHandler health = other.GetComponent<HealthHandler>();
        if (health != null)
        {
            health.AddHealth(-1 * amount);
        }
    }
}
