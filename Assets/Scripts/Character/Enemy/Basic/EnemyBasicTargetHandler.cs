using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicTargetHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Objects detected by this zone will be chased and attacked.")] private GameObjectDetectorZone targetDetectorZone;
    [SerializeField] private LayerMask lineOfSightLayers;
    [SerializeField] private Transform lineOfSightOrigin;
    public GameObject Target { get; private set; }

    private GameObject targetInZone; //gameobject detected by detector zone stored here; actually setting it as target will be determined on a frame-by-frame basis based on line of sight

    public delegate void GameObjectEvent(GameObject gameObject);
    public event GameObjectEvent OnTargetSet;

    private void OnEnable()
    {
        targetDetectorZone.OnObjectEntered += TargetDetectorZone_OnObjectEntered;
        targetDetectorZone.OnObjectExited += TargetDetectorZone_OnObjectExited;
    }

    private void OnDisable()
    {
        targetDetectorZone.OnObjectEntered -= TargetDetectorZone_OnObjectEntered;
        targetDetectorZone.OnObjectExited -= TargetDetectorZone_OnObjectExited;
    }

    private void Update()
    {
        GameObject newTarget = null;
        if (targetInZone != null)
        {
            Vector3 vecToPotential = targetInZone.transform.position - lineOfSightOrigin.position;
            bool hit = Physics.Raycast(new Ray(lineOfSightOrigin.position, vecToPotential), out RaycastHit hitInfo, vecToPotential.magnitude, lineOfSightLayers);
            if (!hit) return;

            GameObject hitObj = hitInfo.collider.gameObject;
            if (hitObj == targetInZone) newTarget = hitObj;
        }

        SetTarget(newTarget);
    }

    public void ClearTarget()
    {
        SetTarget(null);
    }

    private void SetTarget(GameObject target)
    {
        Target = target;
        OnTargetSet?.Invoke(Target);
    }

    private void TargetDetectorZone_OnObjectExited(GameObject obj)
    {
        targetInZone = null;
    }

    private void TargetDetectorZone_OnObjectEntered(GameObject obj)
    {
        targetInZone = obj;
    }
}
