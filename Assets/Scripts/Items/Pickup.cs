using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    public UnityEvent OnPickup;

    public virtual void DoPickup(PickupGrabber pickupGrabber)
    {
        OnPickup?.Invoke();
    }
}
