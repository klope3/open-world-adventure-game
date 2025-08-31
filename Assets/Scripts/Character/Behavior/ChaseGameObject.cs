using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGameObject : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField, Tooltip("The transform of the agent doing the chasing.")] private Transform referenceTransform;
    public GameObject target;

    private void Update()
    {
        if (referenceTransform == null || target == null) return;
        character.SetMovementDirection((target.transform.position - referenceTransform.position).normalized);
    }

    private void OnDisable()
    {
        character.SetMovementDirection(Vector3.zero);
    }
}
