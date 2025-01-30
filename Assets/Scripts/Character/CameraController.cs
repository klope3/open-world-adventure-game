using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    //[SerializeField] private DirectionalInputProvider inputProvider;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private GameObjectDetector targetableDetector;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Vector3 angles;
    private bool isTargeting;
    private Vector3 targetingAngles; //eulerAngles to maintain if targeting without an actual target
    private TargetablePoint targetedPoint; //the point that is currently targeted (null when targeting is off)
    private TargetablePoint pointToBeTargeted; //the point that is waiting to be targeted if the player activates targeting
    private List<TargetablePoint> targetablePoints; //all points that are in range and potentially targetable

    public UnityEvent OnTargetingStart;
    public UnityEvent OnTargetingEnd;
    public System.Action OnTargetingStarted;
    public System.Action OnTargetingEnded;

    private void Awake()
    {
        targetablePoints = new List<TargetablePoint>();
    }

    private void OnEnable()
    {
        targetableDetector.OnObjectEntered += TargetableDetector_OnObjectEntered;
        targetableDetector.OnObjectExited += TargetableDetector_OnObjectExited;
    }
    
    private void OnDisable()
    {
        targetableDetector.OnObjectEntered -= TargetableDetector_OnObjectEntered;
        targetableDetector.OnObjectExited -= TargetableDetector_OnObjectExited;
    }

    public void ToggleTargeting()
    {
        bool prevIsTargeting = isTargeting;
        isTargeting = !isTargeting;

        if (!prevIsTargeting && isTargeting)
        {
            targetingAngles = cameraFollow.eulerAngles;
            OnTargetingStart?.Invoke();
            OnTargetingStarted?.Invoke();
        }
        if (prevIsTargeting && !isTargeting)
        {
            OnTargetingEnd?.Invoke();
            OnTargetingEnded?.Invoke();
        }

        if (!targetedPoint)
        {
            targetedPoint = pointToBeTargeted;
        }
        else
        {
            if (angles.x > 180) angles.x -= 360;
            targetedPoint = null;
        }
    }

    private void TargetableDetector_OnObjectExited(GameObject obj)
    {
        TargetablePoint point = obj.GetComponent<TargetablePoint>();
        if (!point) return;

        ForgetPoint(point);
        point.SetTargetable(false);
    }

    private void TargetableDetector_OnObjectEntered(GameObject obj)
    {
        TargetablePoint point = obj.GetComponent<TargetablePoint>();
        if (!point) return;

        if (!targetablePoints.Contains(point))
        {
            targetablePoints.Add(point);
        }

        point.OnDisabled += Point_OnDisabled;
        point.OnDestroyed += Point_OnDestroyed;
        point.SetTargetable(true);
    }

    private void Point_OnDestroyed(TargetablePoint point)
    {
        ForgetPoint(point);
    }

    private void Point_OnDisabled(TargetablePoint point)
    {
        ForgetPoint(point);
    }

    private void ForgetPoint(TargetablePoint point)
    {
        if (pointToBeTargeted == point) pointToBeTargeted = null;
        targetablePoints.Remove(point);
        point.OnDestroyed -= Point_OnDestroyed;
        point.OnDisabled -= Point_OnDisabled;
    }

    private void Update()
    {
        UpdateTargetables();
        //TargetingInput();

        if (isTargeting)
        {
            if (targetedPoint)
            {
                cameraFollow.LookAt(targetedPoint.transform);
                angles = cameraFollow.eulerAngles;
            } else
            {
                cameraFollow.eulerAngles = targetingAngles;
            }
            return;
        }

        Vector3 initialAngles = new Vector3(angles.x, angles.y, angles.z);
        Vector2 inputVec = InputActionsProvider.InputActions.Player.SecondaryDirectionalAxis.ReadValue<Vector2>();
        angles.x += inputVec.y * sensitivity * Time.deltaTime;
        angles.y += inputVec.x * sensitivity * Time.deltaTime;

        if (angles.x < minX) angles.x = minX;
        if (angles.x > maxX) angles.x = maxX;

        cameraFollow.eulerAngles = angles;
    }

    //private void TargetingInput()
    //{
    //
    //}

    private void UpdateTargetables()
    {
        pointToBeTargeted = ChoosePointToBeTargeted();

        for (int i = 0; i < targetablePoints.Count; i++)
        {
            targetablePoints[i].SetTargetable(targetablePoints[i] == pointToBeTargeted);
        }
    }

    private TargetablePoint ChoosePointToBeTargeted()
    {
        float smallestDist = float.MaxValue;
        TargetablePoint curToBeTargeted = null;

        for (int i = 0; i < targetablePoints.Count; i++)
        {
            float distToThisTargetable = Vector3.Distance(cameraFollow.position, targetablePoints[i].transform.position);
            if (distToThisTargetable < smallestDist)
            {
                curToBeTargeted = targetablePoints[i];
                smallestDist = distToThisTargetable;
            }
        }

        return curToBeTargeted;
    }
}
