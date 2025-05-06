using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

//public class IdleState : PlayerState
//{
//    private InteractionZone interactionZone;
//    private TargetingHandler targetingHandler;
//
//    //default state
//    public override void EnterState()
//    {
//        characterAdapter.canMove = true;
//    }
//
//    public override void UpdateState()
//    {
//        if (InputActionsProvider.GetPrimaryAxis().magnitude >= 0.005f)
//        {
//            stateManager.SwitchState("Moving");
//        }
//    }
//
//    public override void ExitState()
//    {
//    }
//
//    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, InteractionZone interactionZone, TargetingHandler targetingHandler)
//    {
//        Initialize(stateManager, character, characterAdapter);
//        this.interactionZone = interactionZone;
//        this.targetingHandler = targetingHandler;
//    }
//
//    public override void PostInitialize()
//    {
//        InputActionsProvider.OnAButtonStarted += Jump_started;
//        InputActionsProvider.OnBButtonStarted += BButton_started;
//        InputActionsProvider.OnInteractButtonStarted += InteractButton_started;
//        InputActionsProvider.OnZTargetStarted += ZTarget_started;
//    }
//
//    private void ZTarget_started()
//    {
//        if (stateManager.IsInState(this)) targetingHandler.ToggleTargeting();
//    }
//
//    private void InteractButton_started()
//    {
//        if (stateManager.IsInState(this)) interactionZone.Interact();
//    }
//
//    private void BButton_started()
//    {
//        if (!stateManager.IsInState(this)) return;
//
//        stateManager.TryDoAttack();
//    }
//
//    private void Jump_started()
//    {
//        if (!stateManager.IsInState(this)) return;
//
//        character.Jump();
//        stateManager.SwitchState("Jump");
//    }
//
//    public override string GetDebugName()
//    {
//        return "idle";
//    }
//}
