using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : GameState
{
    private float prevTimeScale; //whatever the time scale was before pausing

    public override void EnterState()
    {
        stateManager.NPCManager.SetEnemiesFrozen(true);
        stateManager.GameClock.enabled = false;
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        InputActionsProvider.OnPauseButtonStarted += ToDefaultState;
    }

    public override void ExitState()
    {
        Time.timeScale = prevTimeScale;
        Cursor.lockState = CursorLockMode.Locked;

        InputActionsProvider.OnPauseButtonStarted -= ToDefaultState;
    }

    private void ToDefaultState()
    {
        stateManager.trigger = GameStateManager.DEFAULT_STATE;
    }

    public override string GetDebugName()
    {
        return "Pause";
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
