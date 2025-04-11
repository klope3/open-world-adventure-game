using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] public bool canBePickedUp;
    [SerializeField, Tooltip("Wait this long after enable before automatically becoming pick-uppable.")] 
    private float delayOnEnable;
    [SerializeField, Tooltip("If disabled, will not change pick-uppable status after the delay.")]
    private bool doDelayedEnable;
    public UnityEvent OnPickup;

    public virtual void DoPickup(PickupGrabber pickupGrabber)
    {
        canBePickedUp = false;
        OnPickup?.Invoke();
    }

    private void OnEnable()
    {
        if (doDelayedEnable) StartCoroutine(CO_Enable());
    }

    private IEnumerator CO_Enable()
    {
        yield return new WaitForSeconds(delayOnEnable);
        canBePickedUp = true;
    }
}
