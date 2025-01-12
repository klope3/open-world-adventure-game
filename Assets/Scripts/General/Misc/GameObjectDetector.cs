using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameObjectDetector : MonoBehaviour
{
    public delegate void GameObjectEvent(GameObject obj);
    public event GameObjectEvent OnObjectEntered;
    public event GameObjectEvent OnObjectExited;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnObjectEntered?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnObjectExited?.Invoke(other.gameObject);
    }
}
