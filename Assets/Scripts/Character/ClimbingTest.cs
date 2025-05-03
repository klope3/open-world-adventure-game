using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ECM2;

public class ClimbingTest : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private ECM2CharacterAdapter characterAdapter;
    [SerializeField] private float rayDistance;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private LayerMask raycastLayerMask;
    private Character.MovementMode initialMovementMode;
    private Character.RotationMode initialRotationMode;
    private float initialFlyingFriction;
    private float initialFlySpeed;
    private bool climbing;
    public System.Action OnClimbingStart;
    public System.Action OnClimbingEnd;

    private void Update()
    {
        Vector3 rayStart = character.transform.position + rayOffset;
        Debug.DrawLine(rayStart, rayStart + character.transform.forward * rayDistance, Color.red);
        bool hit = Physics.Raycast(new Ray(rayStart, character.transform.forward), out RaycastHit hitInfo, rayDistance, raycastLayerMask);
        if (hit)
        {
            Debug.DrawLine(hitInfo.point, hitInfo.point + Vector3.up, Color.green);
            Debug.DrawLine(hitInfo.point, hitInfo.point + hitInfo.normal * -1, Color.cyan);
        }

        if (!climbing) return;

        Vector3 primaryAxis = InputActionsProvider.GetPrimaryAxis();

        Vector3 moveVec = new Vector3();
        moveVec.y = primaryAxis.y > 0.05f ? 1 : primaryAxis.y < -0.05f ? -1 : 0;
        character.SetMovementDirection(moveVec);
    }

    private void DetectClimbable()
    {

    }

    [Button]
    public void Activate()
    {
        initialMovementMode = character.movementMode;
        initialRotationMode = character.rotationMode;
        initialFlySpeed = character.maxFlySpeed;
        initialFlyingFriction = character.flyingFriction;

        character.SetMovementMode(Character.MovementMode.Flying);
        character.SetRotationMode(Character.RotationMode.None);
        character.flyingFriction = 10;
        character.maxFlySpeed = 1.5f;
        climbing = true;
        characterAdapter.enabled = false;

        OnClimbingStart?.Invoke();
    }

    [Button]
    public void Deactivate()
    {
        character.SetMovementMode(initialMovementMode);
        character.SetRotationMode(initialRotationMode);
        character.flyingFriction = initialFlyingFriction;
        character.maxFlySpeed = initialFlySpeed;
        climbing = false;
        characterAdapter.enabled = true;

        OnClimbingEnd?.Invoke();
    }
}
