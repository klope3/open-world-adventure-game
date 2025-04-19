using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image overlayImage;
    private static readonly float FADE_DURATION_SLOW = 5f;
    private static readonly float FADE_DURATION_NORMAL = 1f;

    private void Awake()
    {
        overlayImage.color = Color.black;
        FadeIn();
    }

    public void FadeIn()
    {
        overlayImage.DOColor(Color.clear, FADE_DURATION_NORMAL).SetUpdate(true);
    }

    public void FadeOut()
    {
        overlayImage.DOColor(Color.black, FADE_DURATION_NORMAL).SetUpdate(true);
    }

    public void FadeInSlow()
    {
        overlayImage.DOColor(Color.clear, FADE_DURATION_SLOW).SetUpdate(true);
    }

    public void FadeOutSlow()
    {
        overlayImage.DOColor(Color.black, FADE_DURATION_SLOW).SetUpdate(true);
    }
}
