using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicPauseState : EnemyState
{
    private float pauseDuration;
    private float timer;

    public override void EnterState()
    {
        character.maxWalkSpeed = 0;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer > pauseDuration)
        {
            stateManager.SwitchState("Wander");
            timer = 0;
            return;
        }
    }

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, float pauseDuration)
    {
        Initialize(stateManager, character, playerObj, ownTransform);
        this.pauseDuration = pauseDuration;
    }
}
