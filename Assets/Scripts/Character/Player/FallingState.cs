using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public override void EnterState()
    {
        Debug.Log("Entering falling");
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting falling");
    }

    protected override void PostInitialize()
    {
        character.Landed += Character_Landed;
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        stateManager.SwitchState("Idle");
    }
}