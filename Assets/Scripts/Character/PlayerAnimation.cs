using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2CharacterAdapter adapter;
    [SerializeField] private Animator animator;

    private int speedHash;
    private int jumpHash;
    private int landHash;
    private int attackHash;

    private void Awake()
    {
        speedHash = Hash("Speed");
        jumpHash = Hash("Jump");
        landHash = Hash("Land");
        attackHash = Hash("Attack");
    
        character.Jumped += Character_Jumped;
        character.Landed += Character_Landed;
        playerStateManager.OnAttack += PlayerStateManager_OnAttack;
    }

    private void PlayerStateManager_OnAttack()
    {
        animator.SetTrigger(attackHash);
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
        animator.SetFloat(speedHash, adapter.GetMovementInput().magnitude);
    }
    
    private static int Hash(string str)
    {
        return Animator.StringToHash(str);
    }
}
