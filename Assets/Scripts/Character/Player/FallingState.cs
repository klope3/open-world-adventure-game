using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "falling";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.LANDING_STATE, () => stateManager.Character.IsGrounded()),
        };
    }
}
