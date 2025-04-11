using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TargetingHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private TargetingDetectorZone detectorZone;
    [SerializeField] private GameObject targetingIndicator;
    [SerializeField] private CameraController cameraController;
    private GameObject objectToBeTargeted;

    private void Update()
    {
        CheckStopTargeting();

        List<GameObject> objects = detectorZone.GetObjectsList();
        if (objects.Count == 0)
        {
            objectToBeTargeted = null;
            targetingIndicator.SetActive(false);
            return;
        }

        IEnumerable<GameObject> distanceQuery = objects.OrderBy(obj => Vector3.Distance(obj.transform.position, playerTrans.position));
        GameObject closest = distanceQuery.FirstOrDefault();
        objectToBeTargeted = closest; 
        targetingIndicator.SetActive(true);
        targetingIndicator.transform.position = objectToBeTargeted.transform.position;
        TargetablePoint point = objectToBeTargeted.GetComponent<TargetablePoint>();
    }

    private void CheckStopTargeting()
    {
        if (cameraController.TargetingTransform == null) return;

        TargetablePoint currentTargeted = cameraController.TargetingTransform.GetComponent<TargetablePoint>();
        if (currentTargeted == null || currentTargeted.IsTargetable) return;

        cameraController.SetTargetingTransform(null);
    }

    public void ToggleTargeting()
    {
        if (cameraController.TargetingTransform == null)
        {
            if (objectToBeTargeted != null) cameraController.SetTargetingTransform(objectToBeTargeted.transform);
        } else
        {
            cameraController.SetTargetingTransform(null);
        }
    }
}
