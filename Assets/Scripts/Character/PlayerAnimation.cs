using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2CharacterAdapter adapter;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthHandler health;
    [SerializeField] private PlayerClimbingModule climbingModule;

    private int speedHash;
    private int speedXHash;
    private int speedYHash;
    private int jumpHash;
    private int landHash;
    private int attackHash;
    private int attack2Hash;
    private int fallHash;
    private int rollHash;
    private int targetingHash;
    private int dodgeHash;
    private int dieHash;
    private int climbingHash;
    private int climbingMoveLeftHandUpHash;
    private int climbingMoveRightHandUpHash;
    private int climbingMoveLeftHandDownHash;
    private int climbingMoveRightHandDownHash;

    private readonly int LAYER_INDEX_RIGHT_ARM = 1;

    private readonly float RIGHT_ARM_LOWERED_WEIGHT = 0.75f; //currently the right arm layer is only used for lowering the arm while running AND item equipped, so the layer weight will alternate between 0 and this value

    private void Awake()
    {
        speedHash = Hash("Speed");
        jumpHash = Hash("Jump");
        landHash = Hash("Land");
        attackHash = Hash("Attack");
        attack2Hash = Hash("Attack2");
        fallHash = Hash("Fall");
        rollHash = Hash("Roll");
        speedXHash = Hash("SpeedX");
        speedYHash = Hash("SpeedY");
        targetingHash = Hash("Targeting");
        dodgeHash = Hash("Dodge");
        dieHash = Hash("Die");
        climbingHash = Hash("Climbing");
        climbingMoveLeftHandUpHash = Hash("ClimbingMoveLeftHandUp");
        climbingMoveRightHandUpHash = Hash("ClimbingMoveRightHandUp");
        climbingMoveLeftHandDownHash = Hash("ClimbingMoveLeftHandDown");
        climbingMoveRightHandDownHash = Hash("ClimbingMoveRightHandDown");

        character.Jumped += Character_Jumped;
        character.Landed += Character_Landed;
        playerStateManager.OnAttack += PlayerStateManager_OnAttack;
        playerStateManager.OnAttack2 += PlayerStateManager_OnAttack2;
        playerStateManager.OnLeftGround += PlayerStateManager_OnLeftGround;
        playerStateManager.OnRoll += PlayerStateManager_OnRoll;
        playerStateManager.OnDodge += PlayerStateManager_OnDodge;
        playerStateManager.OnClimbingStart += PlayerStateManager_OnClimbingStart;
        playerStateManager.OnClimbingStop += PlayerStateManager_OnClimbingStop;
        climbingModule.OnLeftHandMoveUp += PlayerClimbingModule_OnLeftHandMoveUp;
        climbingModule.OnRightHandMoveUp += PlayerClimbingModule_OnRightHandMoveUp;
        climbingModule.OnLeftHandMoveDown += PlayerClimbingModule_OnLeftHandMoveDown;
        climbingModule.OnRightHandMoveDown += PlayerClimbingModule_OnRightHandMoveDown;
        //climbingModule.OnClimbingUpStart += PlayerClimbingModule_OnClimbingUpStart;
        //climbingModule.OnClimbingUpStop += PlayerClimbingModule_OnClimbingUpStart;
        cameraController.OnTargetingStarted += CameraController_OnTargetingStarted;
        cameraController.OnTargetingEnded += CameraController_OnTargetingEnded;
        health.OnDied += HealthHandler_OnDied;
    }

    private void PlayerClimbingModule_OnLeftHandMoveDown()
    {
        animator.SetTrigger(climbingMoveLeftHandDownHash);
    }

    private void PlayerClimbingModule_OnRightHandMoveDown()
    {
        animator.SetTrigger(climbingMoveRightHandDownHash);
    }

    private void PlayerClimbingModule_OnLeftHandMoveUp()
    {
        animator.SetTrigger(climbingMoveLeftHandUpHash);
    }

    private void PlayerClimbingModule_OnRightHandMoveUp()
    {
        animator.SetTrigger(climbingMoveRightHandUpHash);
    }

    private void PlayerStateManager_OnClimbingStart()
    {
        animator.SetBool(climbingHash, true);
    }

    private void PlayerStateManager_OnClimbingStop()
    {
        animator.SetBool(climbingHash, false);
    }

    private void HealthHandler_OnDied()
    {
        animator.SetTrigger(dieHash);
    }

    private void CameraController_OnTargetingEnded()
    {
        animator.SetBool(targetingHash, false);
    }

    private void CameraController_OnTargetingStarted()
    {
        animator.SetBool(targetingHash, true);
    }

    private void PlayerStateManager_OnLeftGround()
    {
        animator.SetTrigger(fallHash);
    }

    private void PlayerStateManager_OnAttack()
    {
        animator.SetTrigger(attackHash);
    }

    private void PlayerStateManager_OnAttack2()
    {
        animator.SetTrigger(attack2Hash);
    }

    private void PlayerStateManager_OnRoll()
    {
        animator.SetTrigger(rollHash);
    }

    private void PlayerStateManager_OnDodge()
    {
        animator.SetTrigger(dodgeHash);
    }

    public void SetJumpTrigger()
    {
        animator.SetTrigger(jumpHash);
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        animator.SetTrigger(landHash);
    }
    
    private void Character_Jumped()
    {
        animator.SetTrigger(jumpHash);
    }
    
    private void Update()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(inputVec);
        float inputMagnitude = inputVec.magnitude;
        animator.SetFloat(speedXHash, squareVec.x);
        animator.SetFloat(speedYHash, squareVec.y);
        animator.SetFloat(speedHash, inputMagnitude);

        UpdateRightArmLayerWeights();
    }

    private void UpdateRightArmLayerWeights()
    {
        string[] activeInStates = { 
            "Moving",
        }; //the right arm layer should be 0 if the main layer is in any of these states
        if (activeInStates.Contains(playerStateManager.CurrentStateKey)) //this should also depend on other things, like whether a weapon is equipped
        {
            animator.SetLayerWeight(LAYER_INDEX_RIGHT_ARM, RIGHT_ARM_LOWERED_WEIGHT);
        }
        else
        {
            animator.SetLayerWeight(LAYER_INDEX_RIGHT_ARM, 0);
        }
    }
    
    private static int Hash(string str)
    {
        return Animator.StringToHash(str);
    }
}
