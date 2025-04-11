using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup && pickup.canBePickedUp) pickup.DoPickup(this);
    }
}
