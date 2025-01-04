using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStateManager enemyStateManager;
    [SerializeField] private Character character;

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int attackHash = Animator.StringToHash("Attack");

    private void Awake()
    {
        enemyStateManager.OnAttack += EnemyStateManager_OnAttack;
    }

    private void Update()
    {
        animator.SetFloat(speedHash, character.GetMovementDirection().magnitude);
    }

    private void EnemyStateManager_OnAttack()
    {
        animator.SetTrigger(attackHash);
    }
}
