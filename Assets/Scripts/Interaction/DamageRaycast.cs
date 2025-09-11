using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRaycast : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private int amount;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool drawGizmos;

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
    }

    public void Cast()
    {
        bool hit = Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, distance, layerMask);
        if (!hit) return;

        IDamageReceiver damageReceiver = hitInfo.collider.GetComponent<IDamageReceiver>();
        if (damageReceiver == null) return;

        damageReceiver.ReceiveDamage(amount, hitInfo.point);
    }
}
