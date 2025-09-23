using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingBehaviorSO", menuName = "Scriptable Objects/Items/Behaviors/HealingBehaviorSO")]
public class HealingBehaviorSO : ItemBehaviorSO
{
    [field: SerializeField] public int Amount { get; private set; }

    public override void Execute()
    {
        Debug.Log("Heal");
    }
}
