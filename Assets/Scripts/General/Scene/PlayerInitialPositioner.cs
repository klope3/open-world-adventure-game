using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Cinemachine;

public class PlayerInitialPositioner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
        if (sceneTransition == null) Debug.LogError("No SceneTransition found");

        if (spawnPoints.Length == 0 || spawnPoints.Length <= sceneTransition.SpawnPointIndex)
        {
            Debug.LogError($"{sceneTransition.SpawnPointIndex} is not a valid spawn point index for this scene");
        }

        Transform spawnPoint = spawnPoints[sceneTransition.SpawnPointIndex];
        Character playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        playerCharacter.TeleportPosition(spawnPoint.position);
        playerCharacter.TeleportRotation(spawnPoint.rotation);

        CameraController cameraController = FindObjectOfType<CameraController>();
        cameraController.SetCameraAngle(new Vector3(0, spawnPoint.rotation.eulerAngles.y, 0));

        CinemachineVirtualCamera virtualCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.PreviousStateIsValid = false;
    }
}
