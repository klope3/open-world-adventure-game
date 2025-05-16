using System.Collections;
using System.Collections.Generic;

public class DefaultState : PlayerState
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
        return "moving";
    }

    public override StateTransition[] GetTransitions()
    {
        return stateManager.GetDefaultTransitions();
    }
}
