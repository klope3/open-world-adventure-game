using System.Collections;
using System.Collections.Generic;

public abstract class PlayerState : State
{
    protected PlayerStateManager stateManager;

    public void Initialize(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    //is this still needed?
    public abstract void PostInitialize();
}
