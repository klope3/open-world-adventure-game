using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWASD : DirectionalInputProvider
{
    public override Vector2 GetInput()
    {
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(KeyCode.A)) inputX--;
        if (Input.GetKey(KeyCode.D)) inputX++;
        if (Input.GetKey(KeyCode.W)) inputY++;
        if (Input.GetKey(KeyCode.S)) inputY--;

        return new Vector2(inputX, inputY).normalized;
    }
}
