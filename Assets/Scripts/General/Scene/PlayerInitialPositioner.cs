using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Cinemachine;

public class PlayerInitialPositioner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Character playerCharacter;
    [SerializeField] private SceneTransition sceneTransition;

    public void PositionPlayer()
    {
        if (PersistentGameData.sceneTransitionIndex == -1) return;

        if (spawnPoints.Length == 0 || spawnPoints.Length <= PersistentGameData.sceneTransitionIndex)
        {
            Debug.LogError($"{PersistentGameData.sceneTransitionIndex} is not a valid spawn point index for this scene");
        }

        Transform spawnPoint = spawnPoints[PersistentGameData.sceneTransitionIndex];
        playerCharacter.TeleportPosition(spawnPoint.position);
        playerCharacter.TeleportRotation(spawnPoint.rotation);

        CameraController cameraController = FindObjectOfType<CameraController>();
        cameraController.SetCameraAngle(new Vector3(0, spawnPoint.rotation.eulerAngles.y, 0));

        StartCoroutine(CO_CameraTeleport());
    }

    private IEnumerator CO_CameraTeleport()
    {
        yield return new WaitForEndOfFrame();
        virtualCamera.PreviousStateIsValid = false;
    }
}
