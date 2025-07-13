using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private ScreenFade screenFade;

    public void TransitionToScene(string name, int spawnPointIndex)
    {
        PersistentGameData.sceneTransitionIndex = spawnPointIndex;
        StartCoroutine(CO_Transition(name));
    }

    private IEnumerator CO_Transition(string name)
    {
        screenFade.FadeOut();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(name);

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        screenFade.FadeIn();
        yield return new WaitForSecondsRealtime(1);
    }
}
