using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Sirenix.OdinInspector;

//Bridge between ECM2 Character and custom logic
public class ECM2CharacterAdapter : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private CameraController cameraController;
    //[SerializeField] private DirectionalInputProvider inputProvider;
    [SerializeField] public bool canMove = true;
    public Vector3 inputOverride;
    public bool useInputOverride;
    //private InputActions inputActions;
    public System.Action LeftGround; //ECM2 does not seem to provide this event, but we can use some of its methods to easily implement it

    private void Awake()
    {
        //inputActions = new InputActions();
        //inputActions.Player.Enable();
        cameraController.OnTargetingStarted += CameraController_OnTargetingStarted;
        cameraController.OnTargetingEnded += CameraController_OnTargetingEnded;
    }

    private void Update()
    {
        Vector2 inputVec = InputActionsProvider.InputActions.Player.PrimaryDirectionalAxis.ReadValue<Vector2>();
        //Vector2 inputVec = inputProvider.GetInput();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        moveVec = moveVec.relativeTo(character.cameraTransform);
        if (useInputOverride) moveVec = inputOverride;

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

    private void CameraController_OnTargetingStarted()
    {
        character.SetRotationMode(Character.RotationMode.OrientRotationToViewDirection);
    }

    private void CameraController_OnTargetingEnded()
    {
        character.SetRotationMode(Character.RotationMode.OrientRotationToMovement);
    }

    public Vector3 GetMovementInput()
    {
        if (!canMove) return Vector3.zero;
        return InputActionsProvider.InputActions.Player.PrimaryDirectionalAxis.ReadValue<Vector2>();
        //return inputProvider.GetInput();
    }
}
