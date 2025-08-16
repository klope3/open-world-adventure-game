using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpSlashState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "sword up slash";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.TimeInState >= stateManager.SwordUpSlashDuration),
            new StateTransition(PlayerStateManager.LANDING_STATE, () => stateManager.Character.IsGrounded()),
        };
    }
}
