using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver
{
    public void ReceiveDamage(int amount, Vector3 impactPoint);
}
