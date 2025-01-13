using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputActionsProvider
{
    private static InputActions inputActions;
    public static InputActions InputActions
    {
        get
        {
            if (inputActions == null)
            {
                inputActions = new InputActions();
                inputActions.Player.Enable();
            }
            return inputActions;
        }
    }
}
