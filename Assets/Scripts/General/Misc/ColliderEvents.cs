using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public UnityEvent OnTriggerEntered;
    public UnityEvent OnTriggerExited;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke();
    }
}
