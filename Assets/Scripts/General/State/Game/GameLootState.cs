using System.Collections;
using System.Collections.Generic;

public class GameLootState : GameState
{
    public override void EnterState()
    {
        stateManager.NPCManager.SetEnemiesFrozen(true);
        stateManager.GameClock.enabled = false;
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "Loot";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(GameStateManager.DEFAULT_STATE, () => stateManager.trigger == GameStateManager.DEFAULT_STATE),
        };
    }

    public override void UpdateState()
    {
    }
}
