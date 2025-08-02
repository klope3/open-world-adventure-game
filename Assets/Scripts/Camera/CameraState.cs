using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraState : State
{
    protected CameraStateManager stateManager;

    public void Initialize(CameraStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
}
