using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class AttackState : PlayerState
{
    private readonly float DURATION = 0.6f;

    public override void EnterState()
    {
        stateManager.DefaultMovementModule.canMove = false;
        stateManager.IncrementRecentStandardAttacks();

        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        if (stateManager.DefaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly && inputVec.magnitude > 0.05f)
        {
            inputVec = new Vector3(inputVec.x, 0, inputVec.y);
            inputVec = inputVec.relativeTo(stateManager.Character.cameraTransform);
            //when the attack starts, snap character facing direction to the direction of movement input
            //this ensures the player doesn't attack in the wrong direction just because they were in the middle of turning around
            stateManager.Character.SetRotation(Quaternion.LookRotation(inputVec)); 
        }
    }
    
    public override void UpdateState()
    {
    }
    
    public override void ExitState()
    {
        //stateManager.attackInput = false;
        stateManager.DefaultMovementModule.canMove = true;
    }

    public override void PostInitialize()
    {
    }

    public override string GetDebugName()
    {
        return "attack";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(PlayerStateManager.MOVING_STATE, ToDefaultState),
        };
    }

    private bool ToDefaultState()
    {
        return stateManager.TimeInState > DURATION;
    }
}
