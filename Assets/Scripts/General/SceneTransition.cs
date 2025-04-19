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
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void TransitionToScene(string name)
    {
        StartCoroutine(CO_Transition(name));
    }

    private IEnumerator CO_Transition(string name)
    {
        screenFade.FadeOut();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(name);
        player.TeleportPosition(Vector3.zero);
        player.TeleportRotation(Quaternion.identity);
        virtualCamera.PreviousStateIsValid = false;
        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        screenFade.FadeIn();
        yield return new WaitForSecondsRealtime(1);
    }
}
