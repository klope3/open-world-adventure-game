using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class RollState : PlayerState
{
    private float speed;
    private float duration;
    private float initialMoveSpeed;
    private float deceleration;

    public System.Action OnEnter;

    public override void EnterState()
    {
        initialMoveSpeed = character.maxWalkSpeed;

        Vector2 inputVec = characterAdapter.GetMovementInput();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        moveVec = moveVec.relativeTo(character.cameraTransform);
        characterAdapter.inputOverride = moveVec;
        characterAdapter.useInputOverride = true; //lock to whatever the input was at start

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {
        character.maxWalkSpeed = initialMoveSpeed;
        characterAdapter.useInputOverride = false; //release lock on the input
    }

    public override void PostInitialize()
    {

    }

    public override void UpdateState()
    {
        if (stateManager.TimeInState >= duration)
        {
            stateManager.SwitchState("MoveForward");
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
}
