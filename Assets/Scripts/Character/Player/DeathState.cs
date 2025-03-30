using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : PlayerState
{
    public override void EnterState()
    {
        characterAdapter.canMove = false;
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "death";
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
