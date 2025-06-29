using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EquippedItems : MonoBehaviour
{
    [SerializeField] private WeaponSwapper weaponSwapper;
    [SerializeField] private RectTransform primaryRt;
    [SerializeField] private RectTransform secondaryRt;
    [SerializeField] private float swapIconMoveDuration;
    private Tween primaryRtPosTween;
    private Tween secondaryRtPosTween;
    private Tween primaryRtScaleTween;
    private Tween secondaryRtScaleTween;

    private void Awake()
    {
        weaponSwapper.OnSwap += WeaponSwapper_OnSwap;
    }

    private void WeaponSwapper_OnSwap()
    {
        if (primaryRtPosTween != null) primaryRtPosTween.Complete();
        if (secondaryRtPosTween != null) secondaryRtPosTween.Complete();
        if (primaryRtScaleTween != null) primaryRtScaleTween.Complete();
        if (secondaryRtScaleTween != null) secondaryRtScaleTween.Complete();

        Vector2 primaryPos = primaryRt.anchoredPosition;
        Vector2 secondaryPos = secondaryRt.anchoredPosition;
        Vector3 primaryScale = primaryRt.localScale;
        Vector3 secondaryScale = secondaryRt.localScale;

        primaryRtPosTween = primaryRt.DOAnchorPos(secondaryPos, swapIconMoveDuration);
        secondaryRtPosTween = secondaryRt.DOAnchorPos(primaryPos, swapIconMoveDuration);

        primaryRtScaleTween = primaryRt.DOScale(secondaryScale, swapIconMoveDuration);
        secondaryRtScaleTween = secondaryRt.DOScale(primaryScale, swapIconMoveDuration);
    }
}
