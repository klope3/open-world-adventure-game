using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerDeathSequence : MonoBehaviour
{
    [SerializeField] private PlayerDefaultMovementModule characterAdapter;
    [SerializeField] private PlayerStateManager stateManager;
    [SerializeField] private HealthHandler health;
    [SerializeField] private ScreenFade screenFade;
    [SerializeField] private Image[] fadeInImages;
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private TextMeshProUGUI[] fadeInTexts;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Color textFadeToColor;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float activateButtonsDelay;
    [SerializeField] private float timeScale;
    [SerializeField] private AudioSource sound;

    public void DoDeathSequence()
    {
        characterAdapter.canMove = false;
        InputActionsProvider.LockPrimaryAxisTo(Vector3.zero);
        screenFade.FadeOutSlow();
        for (int i = 0; i < fadeInImages.Length; i++)
        {
            Image image = fadeInImages[i];
            image.DOColor(Color.white, fadeDuration).SetUpdate(true);
        }
        StartCoroutine(CO_Buttons());
        Time.timeScale = timeScale;
        Cursor.lockState = CursorLockMode.None;
        sound.Play();
        health.invincible = true;
    }

    private IEnumerator CO_Buttons()
    {
        yield return new WaitForSeconds(activateButtonsDelay);
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
        for (int i = 0; i < buttonImages.Length; i++)
        {
            Image image = buttonImages[i];
            image.DOColor(Color.white, fadeDuration).SetUpdate(true);
        }
        for (int i = 0; i < fadeInTexts.Length; i++)
        {
            TextMeshProUGUI text = fadeInTexts[i];
            text.DOColor(textFadeToColor, fadeDuration).SetUpdate(true);
        }
    }
}