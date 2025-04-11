using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEvents : MonoBehaviour
{
    public delegate void GameObjectEvent(GameObject obj);
    public GameObjectEvent OnDisabled;
    public GameObjectEvent OnDestroyed;

    private void OnDisable()
    {
        OnDisabled?.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(gameObject);
    }
}
