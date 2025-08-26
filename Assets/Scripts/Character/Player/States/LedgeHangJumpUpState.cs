using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeHangJumpUpState : PlayerState
{
    public override void EnterState()
    {
        stateManager.Character.LaunchCharacter(Vector3.up * stateManager.PlayerControlDataSO.LedgeGrabJumpUpForce);
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
        return "ledge jump up";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.Character.velocity.y < 0),
        };
    }
}
