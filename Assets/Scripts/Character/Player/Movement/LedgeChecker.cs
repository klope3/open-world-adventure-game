using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    [SerializeField] private RaycastChecker topChecker;
    [SerializeField] private RaycastChecker topSurfaceChecker;
    [SerializeField] private RaycastChecker bottomChecker;

    public bool LedgeFound()
    {
        return !topChecker.Check() && bottomChecker.Check();
    }

    public void GetLedgeInfo(out RaycastHit bottomRaycastHitInfo, out RaycastHit topSurfaceRaycastHitInfo)
    {
        bottomChecker.CheckWithInfo(out bottomRaycastHitInfo);
        topSurfaceChecker.CheckWithInfo(out topSurfaceRaycastHitInfo);
    }
}
