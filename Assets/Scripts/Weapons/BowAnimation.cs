using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BowAnimation : MonoBehaviour
{
    [SerializeField] private PlayerStateManager stateManager;
    [SerializeField] private SkinnedMeshRenderer bowMeshRenderer;
    public UnityEvent OnPullBackStart;
    public UnityEvent OnQuiverPull;
    public UnityEvent OnShoot;
    public UnityEvent OnCancel;

    private void OnEnable()
    {
        stateManager.OnStateChange += StateManager_OnStateChange;
    }

    private void OnDisable()
    {
        stateManager.OnStateChange -= StateManager_OnStateChange;
    }

    private void StateManager_OnStateChange(string stateName, string prevState)
    {
        if (stateName != PlayerStateManager.MOVING_STATE) return;

        //if (prevState == PlayerStateManager.BOW_HOLD_STATE || prevState == PlayerStateManager.BOW_DRAW_STATE)
        //{
        //    bowMeshRenderer.SetBlendShapeWeight(0, 0);
        //}
        if (prevState == PlayerStateManager.BOW_HOLD_STATE)
        {
            OnShoot?.Invoke();
        }
        if (prevState == PlayerStateManager.BOW_DRAW_STATE)
        {
            OnCancel?.Invoke();
        }
    }

    public void PullBackStart()
    {
        if (stateManager.CurrentStateKey != PlayerStateManager.BOW_DRAW_STATE) return; 
        OnPullBackStart?.Invoke();
    }

    public void QuiverPull()
    {
        if (stateManager.CurrentStateKey != PlayerStateManager.BOW_DRAW_STATE) return;
        OnQuiverPull?.Invoke();
    }
}
