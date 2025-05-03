using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingDetector : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private bool debugVisuals;

    public bool CheckClimbable()
    {
        Vector3 rayStart = character.transform.position + rayOffset;
        if (debugVisuals) Debug.DrawLine(rayStart, rayStart + character.transform.forward * rayDistance, Color.red);
        bool hit = Physics.Raycast(new Ray(rayStart, character.transform.forward), out RaycastHit hitInfo, rayDistance, raycastLayerMask);
        if (hit && debugVisuals)
        {
            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.green);
            Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal * -1, Color.cyan);
        }
        return hit;
    }
}
