using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2CharacterAdapter adapter;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthHandler health;

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
    
        character.Jumped += Character_Jumped;
        character.Landed += Character_Landed;
        playerStateManager.OnAttack += PlayerStateManager_OnAttack;
        playerStateManager.OnAttack2 += PlayerStateManager_OnAttack2;
        playerStateManager.OnLeftGround += PlayerStateManager_OnLeftGround;
        playerStateManager.OnRoll += PlayerStateManager_OnRoll;
        playerStateManager.OnDodge += PlayerStateManager_OnDodge;
        cameraController.OnTargetingStarted += CameraController_OnTargetingStarted;
        cameraController.OnTargetingEnded += CameraController_OnTargetingEnded;
        health.OnDied += HealthHandler_OnDied;
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
        animator.SetFloat(speedXHash, squareVec.x);
        animator.SetFloat(speedYHash, squareVec.y);
        animator.SetFloat(speedHash, inputVec.magnitude);
    }
    
    private static int Hash(string str)
    {
        return Animator.StringToHash(str);
    }
}
