using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] public bool invincible;
    [SerializeField] private bool destroyOnDeath;

    [SerializeField, Tooltip("Can't be hurt for this long after taking damage.")] 
    private float graceTime;

    [SerializeField] private bool showLogs;
    [SerializeField] private bool initializeOnAwake = true;
    private float graceTimer;
    private int curHealth;
    public delegate void PositionEvent(Vector3 position);
    public UnityEvent OnInitialize;
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public System.Action OnHealed;
    public UnityEvent OnDie;
    public System.Action OnDied;
    public event PositionEvent OnDamaged;

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
        if (initializeOnAwake) Initialize();
    }

    public void Initialize(int startingHealth)
    {
        this.startingHealth = startingHealth;
        Initialize();
    }

    public void Initialize()
    {
        curHealth = startingHealth;
        graceTimer = graceTime;
        OnInitialize?.Invoke();
    }

    private void Update()
    {
        if (graceTimer < graceTime)
        {
            graceTimer += Time.deltaTime;
        }
    }

    public void AddHealth(int amount, Vector3 impactPoint)
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
        {
            OnHeal?.Invoke();
            OnHealed?.Invoke();
        }
        if (amount < 0)
        {
            graceTimer = 0;
            OnDamage?.Invoke();
            OnDamaged?.Invoke(impactPoint);
        } 
        if (curHealth == 0)
        {
            OnDie?.Invoke();
            OnDied?.Invoke();
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
