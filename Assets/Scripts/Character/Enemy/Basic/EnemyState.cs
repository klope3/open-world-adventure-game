using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public abstract class EnemyState : State
{
    protected EnemyStateManager stateManager;
    protected MonoBehaviour behavior;

    public void Initialize(EnemyStateManager stateManager, MonoBehaviour behavior)
    {
        this.stateManager = stateManager;
        this.behavior = behavior;
    }
}
