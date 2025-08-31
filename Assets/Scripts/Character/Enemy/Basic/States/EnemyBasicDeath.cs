using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicDeath : EnemyBasicState
{
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override string GetDebugName()
    {
        return "death";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
        };
    }

    public override void UpdateState()
    {

    }
}
