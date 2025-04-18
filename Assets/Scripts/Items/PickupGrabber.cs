using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PickupGrabber : MonoBehaviour
{
    public UnityEvent OnHeartPickup;
    public UnityEvent OnMoneyPickup;

    private void OnTriggerStay(Collider other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (!pickup || !pickup.canBePickedUp) return;

        pickup.DoPickup(this);

        if (pickup is HeartPickup) OnHeartPickup?.Invoke();
        if (pickup is MoneyPickup) OnMoneyPickup?.Invoke();
    }
}
