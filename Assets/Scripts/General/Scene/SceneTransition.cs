using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private ScreenFade screenFade;
    private int spawnPointIndex;
    public int SpawnPointIndex
    {
        get
        {
            return spawnPointIndex;
        }
    }

    public void TransitionToScene(string name, int spawnPointIndex)
    {
        this.spawnPointIndex = spawnPointIndex;
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
