using System.Collections;
using System.Collections.Generic;

public class LootState : PlayerState
{
    public override void EnterState()
    {
        stateManager.Character.enabled = false;
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "loot";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, () => stateManager.trigger == PlayerStateManager.MOVING_STATE),
        };
    }

    public override void PostInitialize()
    {
    }

    public override void UpdateState()
    {

    }
}
