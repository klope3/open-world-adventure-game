using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSlashState : PlayerState
{
    public override void EnterState()
    {
        stateManager.SwordDamageZone.OnDamageAdded += SwordDamageZone_OnDamageAdded;
    }

    private void SwordDamageZone_OnDamageAdded()
    {
        stateManager.Character.LaunchCharacter(Vector3.up * stateManager.SwordDownSlashBounceAmount, true);
    }

    public override void ExitState()
    {
        stateManager.SwordDamageZone.OnDamageAdded -= SwordDamageZone_OnDamageAdded;
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "sword down slash";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.TimeInState >= stateManager.SwordDownSlashDuration),
            new StateTransition(PlayerStateManager.LANDING_STATE, () => stateManager.Character.IsGrounded()),
        };
    }
}
