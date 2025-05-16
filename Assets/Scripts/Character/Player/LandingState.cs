using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LandingState : PlayerState
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "landing";
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {
    }

    public override StateTransition[] GetTransitions()
    {
        StateTransition[] defaultTransitions = stateManager.GetDefaultTransitions();
        StateTransition[] additionalTransitions = new StateTransition[]
        {
            new StateTransition(PlayerStateManager.DEFAULT_STATE, () => stateManager.TimeInState > stateManager.LandingDuration || InputActionsProvider.GetPrimaryAxis().magnitude > 0),
        };
        StateTransition[] allTransitions = defaultTransitions.Concat(additionalTransitions).ToArray();
        return allTransitions;
    }
}
