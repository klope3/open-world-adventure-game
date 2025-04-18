using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : Pickup
{
    [SerializeField, Min(1)] private int amount;

    public override void DoPickup(PickupGrabber pickupGrabber)
    {
        MoneyHandler moneyHandler = pickupGrabber.GetComponent<MoneyHandler>();
        if (moneyHandler) moneyHandler.AddMoney(amount);

        base.DoPickup(pickupGrabber);
    }
}
