using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleTween : MonoBehaviour
{
    [SerializeField] private Vector3 endValue;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    [SerializeField, Tooltip("DOTween documentation seems to say that the Flash ease type uses this value as overshoot instead of amplitude.")] 
    private float overshootOrAmplitude;
    [SerializeField] private float period;

    [Sirenix.OdinInspector.Button]
    public void Run()
    {
        transform.DOScale(endValue, duration).SetEase(ease, overshootOrAmplitude, period);
    }
}
