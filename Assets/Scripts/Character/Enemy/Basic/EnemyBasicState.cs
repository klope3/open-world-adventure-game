using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBasicState : State
{
    protected EnemyBasicStateManager stateManager;

    public void Initialize(EnemyBasicStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
}
