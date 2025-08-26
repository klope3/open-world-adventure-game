using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper for easily playing a specific animation, especially from UnityEvents which don't allow you to pass animation clips as arguments.
/// </summary>
public class PlaySingleAnimation : MonoBehaviour
{
    [SerializeField] private Animancer.AnimancerComponent animancer;
    [SerializeField] private AnimationClip clip;

    [Sirenix.OdinInspector.Button]
    public void Play()
    {
        animancer.Play(clip, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
    }
}
