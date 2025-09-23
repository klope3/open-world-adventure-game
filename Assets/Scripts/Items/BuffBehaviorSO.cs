using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffBehaviorSO", menuName = "Scriptable Objects/Items/Behaviors/BuffBehaviorSO")]
public class BuffBehaviorSO : ItemBehaviorSO
{
    public override void Execute()
    {
        Debug.Log("Buff");
    }
}
