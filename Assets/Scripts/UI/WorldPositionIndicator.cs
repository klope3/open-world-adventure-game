using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Simple component to move a UI element such that it appears to exactly match the position of a point in the 3D world.
//E.g. objective markers, dynamic crosshairs, etc.
public class WorldPositionIndicator : MonoBehaviour
{
    [SerializeField, ShowIf("@!useTargetOverride")] public Transform worldTarget;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera targetCamera;
    [SerializeField, Tooltip("If true, the value of targetOverridePosition will be used instead of worldTarget. Use for representing world positions without needing a Transform to exist for those positions.") ] 
    public bool useTargetOverride;
    [HideInInspector] public Vector3 targetOverridePosition;
    

    private void Update()
    {
        if (worldTarget == null && useTargetOverride == false) return;

        Vector3 worldPosition = useTargetOverride ? targetOverridePosition : worldTarget.position;
        Vector3 screenPos = targetCamera.WorldToScreenPoint(worldPosition);
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRt, screenPos, null, out Vector2 indicatorPos);
        indicator.anchoredPosition = indicatorPos;
    }
}
