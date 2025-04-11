using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetablePoint : MonoBehaviour
{
    [SerializeField] private bool isTargetable = true;

    public bool IsTargetable
    {
        get
        {
            return isTargetable;
        }
    }

    public void SetIsTargetable(bool b)
    {
        isTargetable = b;
    }
}
