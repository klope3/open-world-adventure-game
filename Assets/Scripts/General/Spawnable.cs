using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class Spawnable : MonoBehaviour
{
    public Spawner originSpawner;
    public delegate void SpawnableEvent(Spawnable spawnable);
    public event SpawnableEvent OnDie;

    public void Initialize()
    {
        HealthHandler health = GetComponent<HealthHandler>();
        if (health != null) health.OnDied += HealthHandler_OnDied;
    }

    private void OnDisable()
    {
        //HealthHandler health = GetComponent<HealthHandler>();
        //if (health != null)
        //{
        //    Debug.Log("Unsubscribe", gameObject);
        //}
        //if (health != null) health.OnDied -= HealthHandler_OnDied;
    }

    private void HealthHandler_OnDied()
    {
        OnDie?.Invoke(this);
    }
}
