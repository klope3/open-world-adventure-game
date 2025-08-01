using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastChecker : MonoBehaviour, IVector3Provider
{
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private bool debugVisuals;

    private void Update()
    {
        if (!debugVisuals) return;

        Debug.DrawLine(transform.position, transform.position + transform.forward * rayDistance, Color.red);
        bool hit = DoRaycast(out RaycastHit hitInfo);
        if (hit)
        {
            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.green);
            Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal * -1, Color.cyan);
        }
    }

    public bool Check()
    {
        return DoRaycast(out RaycastHit _);
    }

    public bool CheckWithInfo(out RaycastHit hitInfo)
    {
        return DoRaycast(out hitInfo);
    }

    private bool DoRaycast(out RaycastHit hitInfo)
    {
        return Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, rayDistance, raycastLayerMask);
    }

    public Vector3 GetSurfaceNormal()
    {
        bool hit = DoRaycast(out RaycastHit hitInfo);
        return hit ? hitInfo.normal : Vector3.zero;
    }

    public Vector3 GetVector3()
    {
        bool hit = DoRaycast(out RaycastHit hitInfo);
        return hit ? hitInfo.point : transform.position + transform.forward * rayDistance;
    }
}
