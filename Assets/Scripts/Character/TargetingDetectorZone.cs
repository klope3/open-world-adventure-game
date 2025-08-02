using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingDetectorZone : GameObjectDetectorZone
{
    protected override bool IsObjectValid(GameObject obj)
    {
        if (obj == null) return false;
        TargetablePoint point = obj.GetComponent<TargetablePoint>();
        return point != null && point.gameObject.activeInHierarchy;
    }
}
