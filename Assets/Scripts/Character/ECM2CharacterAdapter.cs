using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

//Bridge between ECM2 Character and custom logic
public class ECM2CharacterAdapter : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private DirectionalInputProvider inputProvider;

    private void Update()
    {
        Vector2 inputVec = inputProvider.GetInput();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        moveVec = moveVec.relativeTo(character.cameraTransform);
        character.SetMovementDirection(moveVec);

        //temp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            character.StopJumping(); //will not be able to jump again until this is called
        }
    }
}
