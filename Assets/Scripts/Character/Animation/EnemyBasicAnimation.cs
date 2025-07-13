using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;

public class EnemyBasicAnimation : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idle;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.") ] private TransitionAssetBase idleTransition;
    [SerializeField] private AnimationClip walk;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.")] private TransitionAssetBase walkTransition;
    [SerializeField] private AnimationClip chase;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.")] private TransitionAssetBase chaseTransition;
    [SerializeField] private AnimationClip attack;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.")] private TransitionAssetBase attackTransition;
    [SerializeField] private AnimationClip recovery;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.")] private TransitionAssetBase recoveryTransition;
    [SerializeField] private AnimationClip hurt;
    [SerializeField, ShowIf("@showTransitionAssets"), Tooltip("If provided, this will be used instead of the animation clip.")] private TransitionAssetBase hurtTransition;
    [SerializeField, Tooltip("Display options to reference transition assets instead of animaction clips.")] private bool showTransitionAssets;
    [SerializeField] private EnemyStateManager enemyStateManager;

    private void OnEnable()
    {
        enemyStateManager.OnStateChange += EnemyStateManager_OnStateChange;
        animancer.Play(idle);
    }

    private void OnDisable()
    {
        enemyStateManager.OnStateChange -= EnemyStateManager_OnStateChange;
    }

    private void EnemyStateManager_OnStateChange(string stateName, string prevState)
    {
        if (stateName == EnemyStateManager.WANDER_STATE) PlayClipOrTransition(walkTransition, walk);
        if (stateName == EnemyStateManager.CHASE_STATE) PlayClipOrTransition(chaseTransition, chase);
        if (stateName == EnemyStateManager.PAUSE_STATE) PlayClipOrTransition(idleTransition, idle);
        if (stateName == EnemyStateManager.RECOVERY_STATE) PlayClipOrTransition(recoveryTransition, recovery);
        if (stateName == EnemyStateManager.ATTACK_STATE) PlayClipOrTransition(attackTransition, attack);
        if (stateName == EnemyStateManager.HURT_STATE) PlayClipOrTransition(hurtTransition, hurt);
    }

    private void PlayClipOrTransition(TransitionAssetBase transition, AnimationClip clip)
    {
        if (transition != null) animancer.Play(transition, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        else animancer.Play(clip, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
    }
}
