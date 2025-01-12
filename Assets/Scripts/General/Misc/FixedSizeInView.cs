using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSizeInView : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float referenceDistance;

    private void Update()
    {
        float curDistance = Vector3.Distance(transform.position, cameraTransform.position);
        transform.localScale = Vector3.one * curDistance / referenceDistance;
    }
}
