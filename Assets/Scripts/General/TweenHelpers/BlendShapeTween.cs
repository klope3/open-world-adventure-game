using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlendShapeTween : MonoBehaviour
{
    [SerializeField] private float endValue;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    [SerializeField, Tooltip("DOTween documentation seems to say that the Flash ease type uses this value as overshoot instead of amplitude.")]
    private float overshootOrAmplitude;
    [SerializeField] private float period;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private int blendShapeIndex;

    [Sirenix.OdinInspector.Button]
    public void Run()
    {
        DOTween.To(() => skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex), (weight) => skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, weight), endValue, duration).SetEase(ease, overshootOrAmplitude, period);
    }
}
