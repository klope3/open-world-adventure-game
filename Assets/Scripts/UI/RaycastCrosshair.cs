using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCrosshair : MonoBehaviour
{
    [SerializeField, Tooltip("The raycast will be emitted from this transform and along this transform's forward vector.")] 
    private Transform raycastSource;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private WorldPositionIndicator crosshairIndicator;

    private void Update()
    {
        Vector3 fallbackPosition = raycastSource.position + raycastSource.forward * raycastDistance; //termination point of the raycast if it doesn't hit anything
        bool hit = Physics.Raycast(new Ray(raycastSource.position, raycastSource.forward), out RaycastHit hitInfo, raycastDistance, layerMask);
        crosshairIndicator.targetOverridePosition = hit ? hitInfo.point : fallbackPosition;
    }
}
