using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicDamageReceiver : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private HealthHandler healthHandler;
    public UnityEvent OnReceiveDamage;
    public delegate void PositionEvent(Vector3 position);
    public event PositionEvent OnReceivedDamage;

    public void ReceiveDamage(int amount, Vector3 impactPoint)
    {
        if (healthHandler != null) healthHandler.AddHealth(-1 * amount, impactPoint);
        OnReceiveDamage?.Invoke();
        OnReceivedDamage?.Invoke(impactPoint);
    }
}
