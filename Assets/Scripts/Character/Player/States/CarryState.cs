using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "carry";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[] { };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
