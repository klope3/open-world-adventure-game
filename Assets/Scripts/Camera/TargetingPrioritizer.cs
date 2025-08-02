using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TargetingPrioritizer : MonoBehaviour
{
    [SerializeField] private TargetingDetectorZone targetingDetectorZone;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private Transform playerTrans;
    public GameObject ObjectToBeTargeted { get; private set; }

    public UnityEvent OnTargetingStart;
    public UnityEvent OnTargetingEnd;
    public System.Action OnTargetingStarted;
    public System.Action OnTargetingEnded;

    private void OnEnable()
    {
        targetingDetectorZone.OnObjectExited += TargetingDetectorZone_OnObjectExited;
    }

    private void TargetingDetectorZone_OnObjectExited(GameObject obj)
    {
        if (ObjectToBeTargeted == obj) ObjectToBeTargeted = null;
    }

    private void Update()
    {
        ObjectToBeTargeted = null;

        List<GameObject> objects = targetingDetectorZone.GetObjectsList();
        if (objects.Count == 0) return;

        IEnumerable<GameObject> distanceQuery = objects.OrderBy(obj => Vector3.Distance(obj.transform.position, playerTrans.position));
        GameObject closest = distanceQuery.FirstOrDefault();
        ObjectToBeTargeted = closest; //a more sophisticated ranking calculation may be better in future
    }
}
