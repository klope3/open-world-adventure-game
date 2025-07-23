using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public Spawner originSpawner;
    public delegate void SpawnableEvent(Spawnable spawnable);
    public event SpawnableEvent OnDie;

    public void OnEnable()
    {
        HealthHandler health = GetComponent<HealthHandler>();
        if (health != null) health.OnDied += HealthHandler_OnDied;
    }

    private void OnDisable()
    {
        HealthHandler health = GetComponent<HealthHandler>();
        if (health != null) health.OnDied -= HealthHandler_OnDied;
    }

    private void HealthHandler_OnDied()
    {
        OnDie?.Invoke(this);
    }
}
