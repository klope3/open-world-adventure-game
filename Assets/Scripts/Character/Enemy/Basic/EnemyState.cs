using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public abstract class EnemyState : State
{
    protected EnemyStateManager stateManager;

    public void Initialize(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
}
