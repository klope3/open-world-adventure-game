using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActionsEvents : MonoBehaviour
{
    //events for when the primary directional axis input changes in a specific way.
    //useful for treating analog stick movement like a digital button press (i.e. moving the stick to move just once to the next element in a menu).
    public event System.Action OnPrimaryDirectionalAxisStartedLeft;
    public event System.Action OnPrimaryDirectionalAxisStartedUp;
    public event System.Action OnPrimaryDirectionalAxisStartedRight;
    public event System.Action OnPrimaryDirectionalAxisStartedDown;
    private Vector2 lastPrimaryDirectionAxisInput;

    private void Update()
    {
        Vector2 newPrimaryDirectionalAxisInput = InputActionsProvider.GetPrimaryAxis();
        if (lastPrimaryDirectionAxisInput.y < 0.005f && newPrimaryDirectionalAxisInput.y >= 0.005f)
        {
            OnPrimaryDirectionalAxisStartedUp?.Invoke();
        }
        if (lastPrimaryDirectionAxisInput.y > -0.005f && newPrimaryDirectionalAxisInput.y <= -0.005f)
        {
            OnPrimaryDirectionalAxisStartedDown?.Invoke();
        }
        if (lastPrimaryDirectionAxisInput.x < 0.005f && newPrimaryDirectionalAxisInput.x >= 0.005f)
        {
            OnPrimaryDirectionalAxisStartedRight?.Invoke();
        }
        if (lastPrimaryDirectionAxisInput.x > -0.005f && newPrimaryDirectionalAxisInput.x <= -0.005f)
        {
            OnPrimaryDirectionalAxisStartedLeft?.Invoke();
        }

        lastPrimaryDirectionAxisInput = newPrimaryDirectionalAxisInput;
    }
}
