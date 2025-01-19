using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public System.Action OnEnter;

    public override void EnterState()
    {
        Debug.Log("Entering falling");
        OnEnter?.Invoke();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        Debug.Log("Exiting falling");
    }

    public override void PostInitialize()
    {
        character.Landed += Character_Landed;
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        stateManager.SwitchState("Idle");
    }
}
