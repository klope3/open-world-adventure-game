using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ECM2;
using Cinemachine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Character player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void TransitionToScene(string name)
    {
        StartCoroutine(CO_Transition(name, Vector3.zero, 0));
    }

    public void TransitionToScene(string name, Vector3 spawnPosition, float spawnRotation)
    {
        StartCoroutine(CO_Transition(name, spawnPosition, spawnRotation));
    }

    private IEnumerator CO_Transition(string name, Vector3 spawnPosition, float spawnRotation)
    {
        screenFade.FadeOut();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(name);

        player.TeleportPosition(spawnPosition);
        Quaternion rot = Quaternion.Euler(new Vector3(0, spawnRotation, 0));
        player.TeleportRotation(rot);

        virtualCamera.PreviousStateIsValid = false;
        cameraController.SetCameraAngle(new Vector3(0, spawnRotation, 0));

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        screenFade.FadeIn();
        yield return new WaitForSecondsRealtime(1);
    }
}
