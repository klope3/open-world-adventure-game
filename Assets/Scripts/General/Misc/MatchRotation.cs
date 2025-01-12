using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField, Tooltip("If provided, the above reference is ignored and the transform with this tag is assigned on Awake.")] 
    private string targetTransformTag;

    private void Awake()
    {
        if (targetTransformTag.Length > 0) targetTransform = GameObject.FindGameObjectWithTag(targetTransformTag).transform;
    }

    private void Update()
    {
        transform.rotation = targetTransform.rotation;
    }
}
