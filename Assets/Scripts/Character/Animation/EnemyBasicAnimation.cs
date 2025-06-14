using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class EnemyBasicAnimation : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip walk;
    [SerializeField] private AnimationClip chase;
    [SerializeField] private AnimationClip attack;
    [SerializeField] private AnimationClip recovery;
    [SerializeField] private AnimationClip hurt;
    [SerializeField] private EnemyStateManager enemyStateManager;

    private void Awake()
    {
        enemyStateManager.OnStateChange += EnemyStateManager_OnStateChange;
    }

    private void OnEnable()
    {
        animancer.Play(idle);
    }

    private void EnemyStateManager_OnStateChange(string stateName)
    {
        if (stateName == EnemyStateManager.WANDER_STATE) animancer.Play(walk, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.CHASE_STATE) animancer.Play(chase, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.PAUSE_STATE) animancer.Play(idle, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.RECOVERY_STATE) animancer.Play(recovery, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.ATTACK_STATE) animancer.Play(attack, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == EnemyStateManager.HURT_STATE) animancer.Play(hurt, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
    }
}
