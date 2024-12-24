using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArrowKeys : DirectionalInputProvider
{
    public override Vector2 GetInput()
    {
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) inputX--;
        if (Input.GetKey(KeyCode.RightArrow)) inputX++;
        if (Input.GetKey(KeyCode.UpArrow)) inputY++;
        if (Input.GetKey(KeyCode.DownArrow)) inputY--;

        return new Vector2(inputX, inputY).normalized;
    }
}
