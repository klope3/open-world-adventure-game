using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RigidbodyLauncher : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float inaccuracy;
    [SerializeField] private Rigidbody rigidbodyToLaunch;
    [SerializeField] private float launchForce;

    [Button]
    public void Launch()
    {
        rigidbodyToLaunch.transform.position = launchPoint.position;
        rigidbodyToLaunch.AddForce(GetInaccurateVector() * launchForce, ForceMode.Impulse);
    }

    private Vector3 GetInaccurateVector()
    {
        Vector3 randOffset = Random.insideUnitCircle * inaccuracy * 0.001f;
        return (launchPoint.right * randOffset.x + launchPoint.up * randOffset.y + launchPoint.forward * 0.001f).normalized;
    }
}
