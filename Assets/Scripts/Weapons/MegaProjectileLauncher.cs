using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class MegaProjectileLauncher : MonoBehaviour
{
    #region References
    [SerializeField] private MegaProjectile projectilePf;
    [SerializeField, ShowIf("@!findPoolByTag")] public GameObjectPool pool;
    [SerializeField, Tooltip("The GameObject to treat as the source of launched projectiles (e.g. the character holding the gun). Useful for " +
        "tracking who hit who with a projectile.")]
    private GameObject projectileSource;
    [SerializeField, ShowIf("@useAmmoModule")] private MegaProjectileAmmoModule ammoModule;
    [SerializeField] private bool useAmmoModule;
    #endregion
    #region Settings
    [SerializeField, ShowIf("@findPoolByTag")] private string poolTag;
    [SerializeField, Tooltip("Useful for instantiated prefabs that don't have a pool reference when created.")] 
    private bool findPoolByTag;
    [SerializeField, Tooltip("If false, the GetTriggerState() function will not be called, and this launcher's trigger state will need " +
        "to be set from outside.")]
    private bool getTriggerState = true;
    [SerializeField] private bool showDebugMessages;
    #endregion
    #region Launcher Behavior
    [SerializeField, Min(0.001f)] private float shotsPerSecond = 1;
    [SerializeField, Min(1)] private int projectilesPerShot = 1;
    [SerializeField, Min(0)] private int startingAmmo;
    [SerializeField, Min(0)] private int maxAmmo;
    [SerializeField, Min(0)] private int ammoPerProjectile;
    [SerializeField, Tooltip("The maximum degrees of inaccuracy for the launch path of each projectile."), Range(0, 90)] 
    private float maxInaccuracy;
    [SerializeField] protected bool isAutomatic;
    #endregion
    #region Private Variables
    [ShowInInspector, DisplayAsString, FoldoutGroup("Internal")] private bool triggerState;
    private bool prevTriggerState;
    private float triggerTimer;
    private Transform launchRefPointParent;
    private Transform launchRefPoint;
    [ShowInInspector, DisplayAsString, FoldoutGroup("Internal")] private int internalAmmo;
    #endregion
    #region UnityEvents
    [FoldoutGroup("Events")] public UnityEvent OnTriggerDown;
    [FoldoutGroup("Events")] public UnityEvent OnTriggerUp;
    [FoldoutGroup("Events")] public UnityEvent OnLaunch;
    [FoldoutGroup("Events")] public UnityEvent OnAmmoChange;
    #endregion
    #region Properties
    public bool TriggerTimerReady { get { return triggerTimer > (1 / shotsPerSecond); } }
    #endregion

    private void OnValidate()
    {
        startingAmmo = Mathf.Clamp(startingAmmo, 0, maxAmmo);
    }

    private void Awake()
    {
        if (findPoolByTag)
            pool = GameObject.FindGameObjectWithTag(poolTag).GetComponent<GameObjectPool>();
        internalAmmo = startingAmmo;

        GameObject go1 = new GameObject("LaunchRefPointParent");
        GameObject go2 = new GameObject("LaunchRefPoint");
        launchRefPointParent = go1.transform;
        launchRefPoint = go2.transform;
        launchRefPointParent.parent = transform;
        launchRefPoint.parent = launchRefPointParent;
        launchRefPointParent.localPosition = Vector3.zero;
        launchRefPoint.localPosition = new Vector3(0, 0, 0.1f);
    }

    private void Update()
    {
        float triggerTimerMax = 1 / shotsPerSecond;

        if (triggerTimer < triggerTimerMax)
            triggerTimer += Time.deltaTime;

        if (getTriggerState)
        {
            prevTriggerState = triggerState;
            SetTriggerState(GetTriggerState());
            if (ShouldLaunch())
                Launch();
        }
        else
        {
            if (ShouldLaunch())
                Launch();
            prevTriggerState = triggerState;
        }
    }

    [Button, LabelText("Force Launch")]
    protected virtual void Launch()
    {
        GameObject pooled = pool.GetPooledObject();
        if (pooled == null)
        {
            ShowDebugMessage("Couldn't get a pooled projectile to launch.");
            return;
        }
        MegaProjectile proj = pooled.GetComponent<MegaProjectile>();
        proj.transform.position = transform.position;
        Vector3 launchVec = GetLaunchVec();
        proj.StartMovement(launchVec, projectileSource);
        triggerTimer = 0;
        AddAmmo(-1 * ammoPerProjectile);

        OnLaunch?.Invoke();
    }

    protected virtual bool GetTriggerState()
    {
        return Input.GetMouseButton(0);
    }

    protected virtual Vector3 GetLaunchVec()
    {
        if (maxInaccuracy == 0)
            return transform.forward;
        return transform.forward;
    }

    public void SetTriggerState(bool state)
    {
        Debug.Log($"Setting {state}");
        triggerState = state;

        if (!prevTriggerState && triggerState)
            ShowDebugMessage("Trigger Down");

        if (!prevTriggerState && triggerState)
            OnTriggerDown?.Invoke();
        if (prevTriggerState && !triggerState)
            OnTriggerUp?.Invoke();
    }

    public void AddAmmo(int amountIn)
    {
        if (amountIn == 0)
            return;
        if (useAmmoModule && ammoModule != null)
        {
            ammoModule.Add(amountIn);
            OnAmmoChange?.Invoke();
            return;
        }
        internalAmmo += amountIn;
        internalAmmo = Mathf.Clamp(internalAmmo, 0, maxAmmo);
        OnAmmoChange?.Invoke();
    }

    /// <summary>
    /// Launch() will be called on every frame in which this returns true.
    /// </summary>
    /// <returns></returns>
    protected virtual bool ShouldLaunch()
    {
        bool autoCanFire = isAutomatic && triggerState;
        bool nonAutoCanFire = !isAutomatic && !prevTriggerState && triggerState;
        //ShowDebugMessage("Ammo: " + HasAmmoForShot + "...Timer Ready: " + TriggerTimerReady + "...Auto Can Fire: " + autoCanFire + "...NonAuto Can Fire: " + nonAutoCanFire);

        return HasAmmoForShot() && TriggerTimerReady && (autoCanFire || nonAutoCanFire);
    }

    public bool HasAmmoForShot()
    {
        return GetCurrentAmmo() >= ammoPerProjectile * projectilesPerShot;
    }

    public int GetCurrentAmmo()
    {
        if (!useAmmoModule)
            return internalAmmo;
        if (ammoModule == null)
            return 0;
        return ammoModule.Ammo;
    }

    public void SetAmmoModule(MegaProjectileAmmoModule moduleIn)
    {
        ammoModule = moduleIn;
        Debug.Log("Ammo module now " + ammoModule);
        OnAmmoChange?.Invoke();
    }

    private void ShowDebugMessage(string message)
    {
        if (showDebugMessages)
            Debug.Log(message);
    }
}
