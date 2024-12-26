using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public abstract class EnemyState : State
{
    protected EnemyStateManager stateManager;
    protected Character character;
    protected GameObject playerObj;
    protected Transform ownTransform;

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform)
    {
        this.stateManager = stateManager;
        this.character = character;
        this.playerObj = playerObj;
        this.ownTransform = ownTransform;
    }
}
