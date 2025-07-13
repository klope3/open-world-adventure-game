using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LookAtPosition : MonoBehaviour
{
    [SerializeField] public Transform transformToModify;
    [SerializeField] public Transform lookAtTransform;
    [SerializeField, Tooltip("A MonoBehaviour that implements IVector3Provider. If assigned, LookAtPosition will use the position from this provider instead of lookAtTransform.")] 
    private MonoBehaviour positionProvider;
    [SerializeField, ShowIf("@constrainLook"), Range(0, 1), Tooltip("Maximum deviance from the initial forward vector.")] 
    private float maxDeviance;
    [SerializeField, ShowIf("@constrainLook"), Tooltip("The deviance will be calculated relative to this transform's forward vector.")] 
    private Transform constraintReference;
    [SerializeField] private bool constrainLook;

    private void OnValidate()
    {
        if (positionProvider != null)
        {
            IVector3Provider vector3Provider = positionProvider.GetComponent<IVector3Provider>();
            if (vector3Provider == null)
            {
                positionProvider = null;
                Debug.LogError("LookAtPosition requires positionProvider to be a script implementing IVector3Provider.");
            }
        }
    }

    private void Update()
    {
        if (transformToModify == null || (lookAtTransform == null && positionProvider == null)) return;

        Vector3 positionToLookAt = positionProvider == null ? lookAtTransform.position : positionProvider.GetComponent<IVector3Provider>().GetVector3();

        Vector3 newForwardVector = (positionToLookAt - transformToModify.position).normalized;
        if (!constrainLook)
        {
            transformToModify.forward = newForwardVector;
            return;
        }

        float deviance = 1 - Vector3.Dot(constraintReference.forward, newForwardVector);
        if (deviance < maxDeviance) transformToModify.forward = newForwardVector;
    }
}
