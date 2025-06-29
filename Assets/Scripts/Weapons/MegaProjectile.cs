using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MegaProjectile : MonoBehaviour
{
    [SerializeField, Min(0.001f)] private float baseSpeed = 100;
    [SerializeField, Min(0.001f)] private float lifespan = 1;
    [SerializeField] private float forceMultiplier;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField, Min(0), Tooltip("The projectile GameObject will stop and stay put, remaining active, for this many seconds after death or impact. " +
        "This allows attached particle systems, audio sources, etc. to perform their functions before the projectile is returned to the pool. If 0, this " +
        "behavior will not be used.")]
    private float stoppedStayTime;
    [SerializeField] private bool faceMoveDirection;
    [SerializeField, Tooltip("Optional reference to a trail renderer. If set, the trail will be cleared when the projectile is launched, helping to prevent visual artifacts.")] 
    private TrailRenderer trailRenderer;
    private Vector3 launchVec;
    private Vector3 moveVec;
    private float curSpeed;
    private float lifeTimer;
    private bool stopped;
    public delegate void ProjectileImpact(MegaProjectile projectile, RaycastHit hitInfo);
    public event ProjectileImpact OnProjectileImpact;
    public UnityEvent OnImpact;
    public UnityEvent OnDeath;
    public GameObject Source { get; private set; }

    public Vector3 CurrentForce { get { return moveVec * curSpeed * forceMultiplier; } }

    private void Update()
    {
        if (stopped)
            return;

        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifespan)
        {
            DoDeath();
            return;
        }

        Vector3 nextPos = GetNextPosition();
        if (TryRaycast(nextPos, out RaycastHit rayHit))
            DoImpact(rayHit);
        else
        {
            if (faceMoveDirection) transform.forward = nextPos - transform.position;
            transform.position = nextPos;
        }
    }

    public virtual void StartMovement(Vector3 vector, GameObject source)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        stopped = false;
        lifeTimer = 0;
        launchVec = vector;
        if (faceMoveDirection) transform.forward = vector;
        moveVec = launchVec;
        curSpeed = baseSpeed;
        Source = source;
        if (trailRenderer != null)
            trailRenderer.Clear();
    }

    protected virtual Vector3 GetNextPosition()
    {
        return transform.position + moveVec * Time.deltaTime * curSpeed;
    }

    private bool TryRaycast(Vector3 nextPos, out RaycastHit rayHit)
    {
        Vector3 vecToNext = nextPos - transform.position;
        bool impact = Physics.Raycast(transform.position, vecToNext, out rayHit, vecToNext.magnitude, collisionLayers);
        return impact;
    }

    protected virtual void DoImpact(RaycastHit rayHit)
    {
        transform.position = rayHit.point;
        MegaProjectileImpactable impactable = rayHit.collider.GetComponent<MegaProjectileImpactable>();
        if (impactable != null)
            impactable.ReceiveProjectile(this, rayHit);
        OnProjectileImpact?.Invoke(this, rayHit);
        OnImpact?.Invoke();
        AttemptStopAndStay();
    }

    protected virtual void DoDeath()
    {
        OnDeath?.Invoke();
        AttemptStopAndStay();
    }

    private void AttemptStopAndStay()
    {
        if (stoppedStayTime > 0)
            StartCoroutine(CO_Stopped());
        else
            gameObject.SetActive(false);
    }

    private IEnumerator CO_Stopped()
    {
        stopped = true;
        yield return new WaitForSeconds(stoppedStayTime);
        gameObject.SetActive(false);
    }
}
