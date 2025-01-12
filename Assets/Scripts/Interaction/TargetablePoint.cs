using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetablePoint : MonoBehaviour
{
    private bool isTargetable;
    public UnityEvent OnBecomeTargetable;
    public UnityEvent OnBecomeNotTargetable;
    public delegate void TargetablePointEvent(TargetablePoint point);
    public event TargetablePointEvent OnDisabled;
    public event TargetablePointEvent OnDestroyed;

    public void SetTargetable(bool b)
    {
        if (b == isTargetable) return;

        isTargetable = b;
        if (isTargetable) OnBecomeTargetable?.Invoke();
        else OnBecomeNotTargetable?.Invoke();  
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    private void OnDisable()
    {
        SetTargetable(false);
        OnDisabled?.Invoke(this);
    }
}
