using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSizeInView : MonoBehaviour
{
    [SerializeField] private float referenceDistance;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (cameraTransform == null) Debug.LogError("Couldn't find main camera!");
    }

    private void Update()
    {
        float curDistance = Vector3.Distance(transform.position, cameraTransform.position);
        transform.localScale = Vector3.one * curDistance / referenceDistance;
    }
}
