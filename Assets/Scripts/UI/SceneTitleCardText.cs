using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SceneTitleCardText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private readonly float FADE_DURATION = 1;
    private readonly float STAY_DURATION = 1;

    public void FadeInText(string text)
    {
        this.text.text = text;
        StartCoroutine(CO_FadeInText());
    }

    private IEnumerator CO_FadeInText()
    {
        DOTween.To(() => text.color, color => text.color = color, Color.white, FADE_DURATION).SetUpdate(true);
        yield return new WaitForSeconds(FADE_DURATION + STAY_DURATION);
        DOTween.To(() => text.color, color => text.color = color, Color.clear, FADE_DURATION).SetUpdate(true);
    }
}
