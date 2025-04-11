using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingDetectorZone : GameObjectDetectorZone
{
    protected override bool IsObjectValid(GameObject obj)
    {
        TargetablePoint point = obj.GetComponent<TargetablePoint>();
        return point != null && point.IsTargetable;
    }
}
