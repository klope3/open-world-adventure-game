using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Randomly chooses to fire the event or not based on the set probability.
public class MaybeEvent : MonoBehaviour
{
    [SerializeField, Range(0, 1), Tooltip("The chance that OnEventInvoked will be invoked when MaybeInvoke is called.")] 
    private float probability;
    public UnityEvent OnEventInvoked;

    public void MaybeInvoke()
    {
        if (Random.Range(0, 1) < probability) OnEventInvoked?.Invoke();
    }
}
