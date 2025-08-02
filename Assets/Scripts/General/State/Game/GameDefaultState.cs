using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Cursor.lockState = CursorLockMode.Locked;

        InputActionsProvider.OnPauseButtonStarted += ToPauseState;
    }

    public override void ExitState()
    {
        InputActionsProvider.OnPauseButtonStarted -= ToPauseState;
    }

    private void ToPauseState()
    {
        if (!stateManager.AllowPauseMenu) return;
        stateManager.trigger = GameStateManager.PAUSE_STATE;
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
            new StateTransition(GameStateManager.PAUSE_STATE, () => stateManager.trigger == GameStateManager.PAUSE_STATE),
        };
    }

    public override void UpdateState()
    {
    }
}
