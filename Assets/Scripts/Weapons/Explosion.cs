using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0.001f)] private float radius;
    [SerializeField, Min(1), Tooltip("The damage something would take if located at the exact center of the explosion. Damage is inversely proportional to distance from the center.")] 
    private int damageAtCenter;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool drawGizmos;
    public UnityEvent OnExplode;

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    [Sirenix.OdinInspector.Button]
    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (Collider col in colliders)
        {
            IDamageReceiver damageReceiver = col.GetComponent<IDamageReceiver>();
            if (damageReceiver == null) continue;

            float dist = Vector3.Distance(col.transform.position, transform.position);
            float slope = -1 * damageAtCenter / radius;
            float damage = dist * slope + damageAtCenter;
            damageReceiver.ReceiveDamage(Mathf.CeilToInt(damage), col.transform.position);
        }
        OnExplode?.Invoke();
    }
}
