using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingIndicator : MonoBehaviour
{
    [SerializeField] private TargetingPrioritizer cameraTargeting;
    [SerializeField] private WorldPositionIndicator worldPositionIndicator;
    [SerializeField] private GameObject indicatorVisual;

    private void Update()
    {
        bool indicatorVisible = cameraTargeting.ObjectToBeTargeted != null;
        indicatorVisual.SetActive(indicatorVisible);
        if (!indicatorVisible) return;

        worldPositionIndicator.targetOverridePosition = cameraTargeting.ObjectToBeTargeted.transform.position;
    }
}
