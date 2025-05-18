using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//currently the detector is very strict and you have to be very close to the surface AND facing it almost perfectly
//find a way to be less strict for better player experience
public class PlayerClimbingDetector : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private bool debugVisuals;

    private void Update()
    {
        DebugVisuals();
    }

    private void DebugVisuals()
    {
        if (!debugVisuals) return;

        Vector3 rayStart = GetRayStart();
        Debug.DrawLine(rayStart, rayStart + character.transform.forward * rayDistance, Color.red);
        bool hit = DoRaycast(out RaycastHit hitInfo);
        if (hit)
        {
            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.green);
            Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal * -1, Color.cyan);
        }
    }

    public bool CheckClimbable()
    {
        return DoRaycast(out RaycastHit _);
    }

    public Vector3 GetClimbingSurfaceNormal()
    {
        bool hit = DoRaycast(out RaycastHit hitInfo);
        return hit ? hitInfo.normal : Vector3.zero;
    }

    private Vector3 GetRayStart()
    {
        return character.transform.position + rayOffset;
    }

    private bool DoRaycast(out RaycastHit hitInfo)
    {
        return Physics.Raycast(new Ray(GetRayStart(), character.transform.forward), out hitInfo, rayDistance, raycastLayerMask);
    }
}
