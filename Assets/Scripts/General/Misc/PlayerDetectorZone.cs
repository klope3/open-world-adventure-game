using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorZone : GameObjectDetectorZone
{
    protected override bool IsObjectValid(GameObject obj)
    {
        return obj.CompareTag("Player");
    }
}
