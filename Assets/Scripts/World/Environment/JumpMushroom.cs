using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class JumpMushroom : MonoBehaviour
{
    [SerializeField] private PlayerDetectorZone detectorZone;
    [SerializeField] private LaunchCharacterZone launchZone;
    private bool windingUp;

    public UnityEvent OnWindUp;

    private readonly Vector3 squishedScale = new Vector3(1.18f, 0.34f, 1.18f);
    private readonly float squishDuration = 0.85f;
    private readonly Ease squishEase = Ease.OutCirc;
    private readonly float bounceOvershoot = 1.8f;
    private readonly float bounceDuration = 0.85f;
    private readonly Ease bounceEase = Ease.OutElastic;

    private void OnEnable()
    {
        detectorZone.OnObjectEntered += DetectorZone_OnObjectEntered;
    }

    private void OnDisable()
    {
        detectorZone.OnObjectEntered -= DetectorZone_OnObjectEntered;
    }

    private void DetectorZone_OnObjectEntered(GameObject obj)
    {
        if (!windingUp) WindUp();
    }

    //the mushroom prepares to launch
    private void WindUp()
    {
        windingUp = true;
        transform.DOScale(squishedScale, squishDuration).SetEase(squishEase).OnComplete(Launch);
        OnWindUp?.Invoke();
    }

    private void Launch()
    {
        windingUp = false;
        transform.DOScale(Vector3.one, bounceDuration).SetEase(bounceEase, bounceOvershoot);
        launchZone.Launch();
    }
}
