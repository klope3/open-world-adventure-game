using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LookAtTransform : MonoBehaviour
{
    [SerializeField] public Transform transformToModify;
    [SerializeField] public Transform lookAtTransform;
    [SerializeField, ShowIf("@constrainLook"), Range(0, 1), Tooltip("Maximum deviance from the initial forward vector.")] 
    private float maxDeviance;
    [SerializeField, ShowIf("@constrainLook"), Tooltip("The deviance will be calculated relative to this transform's forward vector.")] 
    private Transform constraintReference;
    [SerializeField] private bool constrainLook;

    private void Update()
    {
        if (lookAtTransform == null || transformToModify == null) return;

        Vector3 newForwardVector = (lookAtTransform.position - transformToModify.position).normalized;
        if (!constrainLook)
        {
            transformToModify.forward = newForwardVector;
            return;
        }

        float deviance = 1 - Vector3.Dot(constraintReference.forward, newForwardVector);
        if (deviance < maxDeviance) transformToModify.forward = newForwardVector;
    }
}
