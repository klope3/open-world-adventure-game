using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvents : MonoBehaviour
{
    public UnityEvent OnCollisionEntered;
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

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEntered?.Invoke();
    }
}
