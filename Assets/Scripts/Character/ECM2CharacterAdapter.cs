using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

//Bridge between ECM2 Character and custom logic
public class ECM2CharacterAdapter : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private DirectionalInputProvider inputProvider;
    [SerializeField] public bool canMove = true;
    public System.Action LeftGround; //ECM2 does not seem to provide this event, but we can use some of its methods to easily implement it

    private void Update()
    {
        Vector2 inputVec = inputProvider.GetInput();
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

    public Vector3 GetMovementInput()
    {
        return inputProvider.GetInput();
    }
}
