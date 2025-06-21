using System.Collections;
using System.Collections.Generic;

//state the game is in while player is running around, fighting, etc.
public class GameDefaultState : GameState
{
    public override void EnterState()
    {
        stateManager.PlayerStateManager.enabled = true;
        stateManager.PlayerCharacter.enabled = true;
        stateManager.PlayerAnimator.enabled = true;
        stateManager.NPCManager.SetEnemiesFrozen(false);
        stateManager.GameClock.enabled = true;
        stateManager.CameraController.enabled = true;
    }

    public override void ExitState()
    {
    }

    public override string GetDebugName()
    {
        return "Default";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(GameStateManager.LOOT_STATE, () => stateManager.trigger == GameStateManager.LOOT_STATE),
        };
    }

    public override void UpdateState()
    {
    }
}
