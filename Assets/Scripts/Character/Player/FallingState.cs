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
        stateManager.Character.Landed += Character_Landed;
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        //if (InputActionsProvider.GetPrimaryAxis().magnitude <= 0.005f) stateManager.ToDefaultState();
        //else stateManager.ToDefaultState();
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

    private bool ToDefaultState()
    {
        return stateManager.Character.IsGrounded();
    }
}
