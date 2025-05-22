using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingReachTopState : PlayerState
{
    public override void EnterState()
    {
        stateManager.Character.enabled = false;
    }

    public override void ExitState()
    {
        stateManager.Character.enabled = true;
    }

    public override string GetDebugName()
    {
        return "climbing reach top";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, () => stateManager.TimeInState >= stateManager.ClimbingReachTopDuration),
        };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
