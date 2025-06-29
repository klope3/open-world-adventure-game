using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform testTransform;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private PlayerStateManager stateManager;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private bool lockCursor;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera defaultVirtualCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera lootingVirtualCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera bowCamera;
    private ActiveCamera activeCamera;
    private Vector3 angles;
    private Transform targetingTransform; //the point that is currently being targeted (null when targeting is off)

    public UnityEvent OnTargetingStart;
    public UnityEvent OnTargetingEnd;
    public System.Action OnTargetingStarted;
    public System.Action OnTargetingEnded;

    public Transform TargetingTransform
    {
        get
        {
            return targetingTransform;
        }
    }

    public enum ActiveCamera
    {
        Default,
        Looting,
        Bow,
    }

    private void Awake()
    {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked;
        stateManager.OnStateChange += StateManager_OnStateChange;

    }

    private void StateManager_OnStateChange(string stateName, string prevState)
    {
        if (stateName == PlayerStateManager.BOW_HOLD_STATE) SetActiveCamera(ActiveCamera.Bow);
        if (stateName == PlayerStateManager.DEFAULT_STATE) SetActiveCamera(ActiveCamera.Default);
    }

    public void SetTargetingTransform(Transform targetingTransform)
    {
        Transform prevTargetingTransform = this.targetingTransform;
        this.targetingTransform = targetingTransform;

        if (prevTargetingTransform == null && this.targetingTransform != null)
        {
            OnTargetingStart?.Invoke();
            OnTargetingStarted?.Invoke();
        }
        if (prevTargetingTransform != null && this.targetingTransform == null)
        {
            OnTargetingEnd?.Invoke();
            OnTargetingEnded?.Invoke();
        }
    }

    public void SetCameraAngle(Vector3 angles)
    {
        this.angles = angles;
    }

    private void Update()
    {
        if (targetingTransform != null)
        {
            cameraFollow.LookAt(targetingTransform);
            angles = cameraFollow.eulerAngles;
            return;
        }
        
        Vector2 inputVec = InputActionsProvider.GetSecondaryAxis();
        angles.x += inputVec.y * sensitivity * Time.deltaTime;
        angles.y += inputVec.x * sensitivity * Time.deltaTime;

        if (angles.x > 180) angles.x -= 360;
        if (angles.x < minX) angles.x = minX;
        if (angles.x > maxX) angles.x = maxX;

        cameraFollow.eulerAngles = angles;
    }

    public void SetActiveCamera(ActiveCamera activeCamera)
    {
        this.activeCamera = activeCamera;

        defaultVirtualCamera.Priority = this.activeCamera == ActiveCamera.Default ? 1 : 0;
        lootingVirtualCamera.Priority = this.activeCamera == ActiveCamera.Looting ? 1 : 0;
        bowCamera.Priority = this.activeCamera == ActiveCamera.Bow ? 1 : 0;
    }
}
