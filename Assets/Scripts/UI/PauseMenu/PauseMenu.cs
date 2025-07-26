using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    public UnityEvent OnPause;
    public UnityEvent OnUnpause;

    public void Initialize()
    {
        gameStateManager.OnStateChange += GameStateManager_OnStateChange;
    }

    private void GameStateManager_OnStateChange(string newState, string prevState)
    {
        if (newState == GameStateManager.PAUSE_STATE)
        {
            gameObject.SetActive(true);
            OnPause?.Invoke();
        }
        if (newState == GameStateManager.DEFAULT_STATE)
        {
            gameObject.SetActive(false);
            OnUnpause?.Invoke();
        }
    }

    public void Unpause()
    {
        gameStateManager.trigger = GameStateManager.DEFAULT_STATE;
    }

    public void SaveAndQuit()
    {
        bool saved = GameSaver.TrySave();
        if (!saved) return;

        sceneTransition.TransitionToScene(mainMenuSceneName, 0);
    }
}
