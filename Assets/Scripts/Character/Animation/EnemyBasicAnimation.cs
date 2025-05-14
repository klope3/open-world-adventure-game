using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Animancer;

public class EnemyBasicAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip walk;
    [SerializeField] private AnimationClip attack;
    [SerializeField] private EnemyStateManager enemyStateManager;
    [SerializeField] private Character character;

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int attackHash = Animator.StringToHash("Attack");
    private readonly int pauseHash = Animator.StringToHash("Pause");

    private void Awake()
    {
        enemyStateManager.OnStateChange += EnemyStateManager_OnStateChange;
        //enemyStateManager.OnAttack += EnemyStateManager_OnAttack;
        //enemyStateManager.OnPause += EnemyStateManager_OnPause;
    }

    private void EnemyStateManager_OnStateChange(string stateName)
    {
        if (stateName == EnemyStateManager.WANDER_STATE) animancer.Play(walk, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.CHASE_STATE) animancer.Play(walk, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.PAUSE_STATE) animancer.Play(idle, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.ATTACK_STATE) animancer.Play(attack, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
    }

    private void Update()
    {
        animator.SetFloat(speedHash, character.GetMovementDirection().magnitude);
    }

    private void EnemyStateManager_OnAttack()
    {
        animator.SetTrigger(attackHash);
    }

    private void EnemyStateManager_OnPause()
    {
        animator.SetTrigger(pauseHash);
    }
}
