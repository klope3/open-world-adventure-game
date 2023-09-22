using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField, Min(0.001f)] private float timesPerSecond;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        timerMax = 1 / timesPerSecond;
    }

    private void OnEnable()
    {
        DoDamage();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
            DoDamage();
    }

    private void DoDamage()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale * 0.5f, transform.rotation);
        foreach (Collider hit in colliders)
        {
            HealthHandler health = hit.GetComponent<HealthHandler>();
            if (health != null)
            {
                health.AddHealth(-1 * amount);
            }
        }
        timer = 0;
    }
}
