using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool invincible;
    [SerializeField] private bool destroyOnDeath;

    [SerializeField, Tooltip("Can't be hurt for this long after taking damage.")] 
    private float graceTime;

    [SerializeField] private bool showLogs;
    private float graceTimer;
    private int curHealth;
    public UnityEvent OnAwake;
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDie;

    public int CurHealth
    {
        get
        {
            return curHealth;
        }
    }
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    private void Awake()
    {
        curHealth = startingHealth;
        graceTimer = graceTime;
        OnAwake?.Invoke();
    }

    private void Update()
    {
        if (graceTimer < graceTime)
        {
            graceTimer += Time.deltaTime;
        }
    }

    public void AddHealth(int amount)
    {
        if (amount == 0 || (amount < 0 && invincible))
            return;
        if (amount < 0 && graceTimer < graceTime)
            return;

        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        if (showLogs)
            Debug.Log($"Added {amount} to {gameObject.name}'s health");

        if (amount > 0)
            OnHeal?.Invoke();
        if (amount < 0)
        {
            graceTimer = 0;
            OnDamage?.Invoke();
        } 
        if (curHealth == 0)
        {
            OnDie?.Invoke();
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
