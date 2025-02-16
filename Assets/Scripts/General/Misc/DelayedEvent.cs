using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    [SerializeField] private float delaySeconds;
    public UnityEvent OnDelayEnd;

    public void StartDelay()
    {
        StartCoroutine(CO_Delay());
    }

    private IEnumerator CO_Delay()
    {
        yield return new WaitForSeconds(delaySeconds);
        OnDelayEnd?.Invoke();
    }
}
