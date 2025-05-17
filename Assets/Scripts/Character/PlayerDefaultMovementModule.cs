using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Sirenix.OdinInspector;

public class PlayerDefaultMovementModule : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private CameraController cameraController;
    [SerializeField] public bool canMove = true;
    private MovementType movementType;
    public System.Action LeftGround; //ECM2 does not seem to provide this event, but we can use some of its methods to easily implement it

    public MovementType CurrentMovementType
    {
        get
        {
            return movementType;
        }
    }

    public enum MovementType
    {
        ForwardOnly, //only animate between idle, walk forward, and run forward (assumes character's body is always facing movement direction)
        Strafe //forward, backward, strafe, and in-between animations (for when character's body doesn't necessarily face movement direction)
    }

    private void Awake()
    {
        SetMovementType(MovementType.ForwardOnly);
        cameraController.OnTargetingStarted += CameraController_OnTargetingStarted;
        cameraController.OnTargetingEnded += CameraController_OnTargetingEnded;
    }

    private void Update()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        moveVec = moveVec.relativeTo(character.cameraTransform);

        if (canMove)
        {
            character.SetMovementDirection(moveVec);
        } else
        {
            character.SetMovementDirection(Vector3.zero);
        }

        if (character.WasGrounded() && !character.IsGrounded())
        {
            LeftGround?.Invoke();
        }
    }

    [Button]
    public void SetMovementType(MovementType type)
    {
        movementType = type;
        character.SetRotationMode(type == MovementType.Strafe ? Character.RotationMode.OrientRotationToViewDirection : Character.RotationMode.OrientRotationToMovement);
    }

    private void CameraController_OnTargetingStarted()
    {
        character.SetRotationMode(Character.RotationMode.OrientRotationToViewDirection);
    }

    private void CameraController_OnTargetingEnded()
    {
        character.SetRotationMode(Character.RotationMode.OrientRotationToMovement);
    }
}
