using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : State
{
    protected GameStateManager stateManager;

    public void Initialize(GameStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
}
