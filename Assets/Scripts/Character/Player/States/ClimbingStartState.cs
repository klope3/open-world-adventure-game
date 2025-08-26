using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingStartState : PlayerState
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
        return "climbing start";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.CLIMBING_STATE, () => stateManager.TimeInState >= stateManager.PlayerControlDataSO.ClimbingStartDuration),
        };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
