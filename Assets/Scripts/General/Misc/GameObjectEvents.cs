using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvents : MonoBehaviour
{
    public delegate void GameObjectEvent(GameObject obj);
    public GameObjectEvent OnEnabled;
    public GameObjectEvent OnDisabled;
    public GameObjectEvent OnDestroyed;

    public UnityEvent OnBecomeEnabled;

    private void OnEnable()
    {
        OnEnabled?.Invoke(gameObject);
        OnBecomeEnabled?.Invoke();
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(gameObject);
    }
}
