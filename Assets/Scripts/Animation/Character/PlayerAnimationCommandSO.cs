using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerAnimationCommandSO", menuName = "Scriptable Objects/PlayerAnimationCommandSO")]
public class PlayerAnimationCommandSO : ScriptableObject
{
    [field: SerializeField] public AnimationClip BaseAnimation;
    [field: SerializeField] public TransitionAssetBase BaseTransitionAsset;
    [field: SerializeField] public float TorsoLayerWeight;
    [field: SerializeField] public AnimationClip TorsoAnimation;
    [field: SerializeField] public TransitionAssetBase TorsoTransitionAsset;
    [field: SerializeField, ShowIf("@UseCustomFadeDuration")] public float FadeDuration = MiscConstants.DEFAULT_ANIMATION_BLEND_TIME;
    [field: SerializeField] public bool UseCustomFadeDuration;
    [field: SerializeField] public bool UseRootMotion;
    [field: SerializeField] public PlayerAnimation.UpdateType UpdateType;
    [field: SerializeField] public PlayerAnimation.SpecialFunction SpecialFunction;
}
