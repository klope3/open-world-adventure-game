using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class DodgeState : PlayerState
{
    private float speed;
    private float duration;
    private float deceleration;
    private float initialMoveSpeed;
    private float initialAcceleration;

    public System.Action OnEnter;

    public override void EnterState()
    {
        initialMoveSpeed = character.maxWalkSpeed;
        initialAcceleration = character.maxAcceleration;

        Vector2 inputVec = InputActionsProvider.GetPrimaryAxis();
        InputActionsProvider.LockPrimaryAxisTo(inputVec);

        character.maxAcceleration = 1000; //max mobility during dodge

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        character.maxWalkSpeed = initialMoveSpeed;
        character.maxAcceleration = initialAcceleration;
        InputActionsProvider.UnlockPrimaryAxis();
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        if (stateManager.TimeInState >= duration)
        {
            stateManager.SwitchState("Moving");
            return;
        }
        character.maxWalkSpeed = deceleration * stateManager.TimeInState + speed; //deceleration while rolling
    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, float speed, float duration, float deceleration)
    {
        Initialize(stateManager, character, characterAdapter);
        this.speed = speed;
        this.duration = duration;
        this.deceleration = deceleration;
    }

    public override string GetDebugName()
    {
        return "dodge";
    }
}
