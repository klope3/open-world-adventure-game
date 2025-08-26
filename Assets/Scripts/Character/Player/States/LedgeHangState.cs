using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

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
        Vector3 newPos = refPoint + bottomRaycastHitInfo.normal * stateManager.PlayerControlDataSO.LedgeGrabDepthOffset + Vector3.down * stateManager.PlayerControlDataSO.LedgeGrabHeightOffset;
        stateManager.Character.TeleportPosition(newPos);
        stateManager.Character.TeleportRotation(Quaternion.LookRotation(bottomRaycastHitInfo.normal * -1));

        InputActionsProvider.OnAButtonStarted += InputActionsProvider_OnAButtonStarted;
    }

    private void InputActionsProvider_OnAButtonStarted()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        inputVec = new Vector3(inputVec.x, 0, inputVec.y);
        inputVec = inputVec.relativeTo(stateManager.Character.cameraTransform);
        float forwardness = Vector3.Dot(stateManager.Character.transform.forward, inputVec);
        //if (1 - forwardness < stateManager.LedgeGrabJumpTolerance) stateManager.Character.LaunchCharacter(Vector3.up * stateManager.LedgeGrabJumpUpForce);
        if (1 - forwardness < stateManager.PlayerControlDataSO.LedgeGrabJumpTolerance)
        {
            stateManager.trigger = PlayerStateManager.LEDGE_JUMP_UP_STATE;
        } else
        {
            stateManager.trigger = PlayerStateManager.FALLING_STATE;
        }
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
            new StateTransition(PlayerStateManager.LEDGE_JUMP_UP_STATE, () => stateManager.trigger == PlayerStateManager.LEDGE_JUMP_UP_STATE),
        };
    }
}
