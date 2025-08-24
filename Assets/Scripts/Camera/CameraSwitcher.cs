using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerStateManager playerStateManager;
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera DefaultVirtualCamera { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera LootingVirtualCamera { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera BowCamera { get; private set; }
    private ActiveCamera activeCamera;

    public enum ActiveCamera
    {
        Default,
        Looting,
        Bow
    }

    private void OnEnable()
    {
        playerStateManager.OnStateChange += PlayerStateManager_OnStateChange;
    }

    private void OnDisable()
    {
        playerStateManager.OnStateChange -= PlayerStateManager_OnStateChange;
    }

    private void PlayerStateManager_OnStateChange(string newState, string prevState)
    {
        if (newState == PlayerStateManager.BOW_HOLD_STATE) SetActiveCamera(ActiveCamera.Bow);
        if (newState == PlayerStateManager.MOVING_STATE) SetActiveCamera(ActiveCamera.Default);
    }

    public void SetActiveCamera(ActiveCamera activeCamera)
    {
        this.activeCamera = activeCamera;

        DefaultVirtualCamera.Priority = this.activeCamera == ActiveCamera.Default ? 1 : 0;
        LootingVirtualCamera.Priority = this.activeCamera == ActiveCamera.Looting ? 1 : 0;
        BowCamera.Priority = this.activeCamera == ActiveCamera.Bow ? 1 : 0;
    }
}
