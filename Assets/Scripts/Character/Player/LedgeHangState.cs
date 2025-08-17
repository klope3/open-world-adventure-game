using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeHangState : PlayerState
{
    private float initialGravityScale;

    public override void EnterState()
    {
        initialGravityScale = stateManager.Character.gravityScale;
        stateManager.Character.gravityScale = 0;
        stateManager.Character.SetVelocity(Vector3.zero);
        stateManager.DefaultMovementModule.canMove = false;

        stateManager.LedgeChecker.GetLedgeInfo(out RaycastHit bottomRaycastHitInfo, out RaycastHit topSurfaceRaycastHitInfo);
        Vector3 vertSurfaceHitPoint = bottomRaycastHitInfo.point;
        Vector3 horzSurfaceHitPoint = topSurfaceRaycastHitInfo.point;
        Vector3 refPoint = new Vector3(vertSurfaceHitPoint.x, horzSurfaceHitPoint.y, vertSurfaceHitPoint.z);
        Vector3 newPos = refPoint + bottomRaycastHitInfo.normal * stateManager.LedgeGrabDepthOffset + Vector3.down * stateManager.LedgeGrabHeightOffset;
        stateManager.Character.TeleportPosition(newPos);
        stateManager.Character.TeleportRotation(Quaternion.LookRotation(bottomRaycastHitInfo.normal * -1));

        InputActionsProvider.OnAButtonStarted += InputActionsProvider_OnAButtonStarted;
    }

    private void InputActionsProvider_OnAButtonStarted()
    {
        stateManager.trigger = PlayerStateManager.FALLING_STATE;
    }

    public override void ExitState()
    {
        stateManager.Character.gravityScale = initialGravityScale;
        stateManager.DefaultMovementModule.canMove = true;
        InputActionsProvider.OnAButtonStarted -= InputActionsProvider_OnAButtonStarted;
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
    }

    public override string GetDebugName()
    {
        return "ledge hang";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.FALLING_STATE, () => stateManager.trigger == PlayerStateManager.FALLING_STATE),
        };
    }
}
