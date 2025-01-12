using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CinemaBars : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    [SerializeField] private float lerpTime;
    private float startingYScale; //expects bars to be scaled on awake such that they are offscreen

    private void Awake()
    {
        startingYScale = rt.localScale.y;
    }

    public void SetBarsVisibility(bool b)
    {
        if (b) rt.DOScaleY(1, lerpTime);
        else rt.DOScaleY(startingYScale, lerpTime);
    }
}
