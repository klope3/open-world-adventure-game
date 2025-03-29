using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartPickup : Pickup
{
    public override void DoPickup(PickupGrabber pickupGrabber)
    {
        HealthHandler health = pickupGrabber.GetComponent<HealthHandler>();
        if (health) health.AddHealth(4, transform.position);

        base.DoPickup(pickupGrabber);
    }
}
