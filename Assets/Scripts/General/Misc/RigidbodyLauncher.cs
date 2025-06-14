using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RigidbodyLauncher : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float inaccuracy;
    [SerializeField, Tooltip("If set, will get the rigidbody from the pool and launch that instead.")] 
    private GameObjectPool poolToUse;
    [SerializeField] private Rigidbody rigidbodyToLaunch;
    [SerializeField] private float launchForce;

    [Button]
    public void Launch()
    {
        Rigidbody rb = GetRigidbody();
        rb.transform.position = launchPoint.position;
        rb.velocity = GetInaccurateVector() * launchForce; //set velocity directly to cancel out any previous momentum
    }

    private Vector3 GetInaccurateVector()
    {
        Vector3 randOffset = Random.insideUnitCircle * inaccuracy * 0.001f;
        return (launchPoint.right * randOffset.x + launchPoint.up * randOffset.y + launchPoint.forward * 0.001f).normalized;
    }

    private Rigidbody GetRigidbody()
    {
        if (poolToUse == null) return rigidbodyToLaunch;

        GameObject go = poolToUse.GetPooledObject();
        go.SetActive(true);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        return rb;
    }

    public void SetGameObjectPool(GameObjectPool pool)
    {
        poolToUse = pool;
    }
}
