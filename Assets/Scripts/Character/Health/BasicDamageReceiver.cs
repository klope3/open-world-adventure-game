using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicDamageReceiver : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField, Tooltip("After ReceiveDamage is called, further calls will be ignored for this many seconds.")] 
    private float delayTime;
    private bool active = true;
    public UnityEvent OnReceiveDamage;
    public delegate void PositionEvent(Vector3 position);
    public event PositionEvent OnReceivedDamage;

    public void ReceiveDamage(int amount, Vector3 impactPoint)
    {
        if (!active) return;

        if (healthHandler != null) healthHandler.AddHealth(-1 * amount, impactPoint);
        OnReceiveDamage?.Invoke();
        OnReceivedDamage?.Invoke(impactPoint);
        if (delayTime > 0) StartCoroutine(CO_Delay());
    }

    private IEnumerator CO_Delay()
    {
        active = false;
        yield return new WaitForSeconds(delayTime);
        active = true;
    }
}
