using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraStateEvents : MonoBehaviour
{
    [SerializeField] private CameraStateManager cameraStateManager;
    public UnityEvent OnCameraLocked;
    public UnityEvent OnCameraTargeted;
    public UnityEvent OnCameraUnlocked;

    public void Initialize()
    {
        cameraStateManager.OnStateChange += CameraStateManager_OnStateChange;
    }

    private void CameraStateManager_OnStateChange(string newState, string prevState)
    {
        if (newState == CameraStateManager.LOCKED_STATE) OnCameraLocked?.Invoke();
        if (newState == CameraStateManager.TARGETING_STATE) OnCameraTargeted?.Invoke();
        if (newState == CameraStateManager.DEFAULT_STATE && prevState != null) OnCameraUnlocked?.Invoke();
    }
}
