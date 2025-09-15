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
    public event System.Action OnDamageAdded;

    private void OnTriggerStay(Collider other)
    {
        IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            Vector3 point = other.ClosestPoint(transform.position);
            damageReceiver.ReceiveDamage(amount, point);
            OnDamageAdded?.Invoke();
        }

        Character character = other.GetComponent<Character>();
        if (character != null && force > 0 && forceOrigin != null)
        {
            character.AddExplosionForce(force, forceOrigin.position, 1000, 0, ForceMode.Impulse);
        }
    }
}
