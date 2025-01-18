using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private RectTransform boxRt;
    [SerializeField] private Image boxImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float shrinkScaleX;
    [SerializeField] private float shrinkScaleY;
    [SerializeField] private float printTextDelay;
    [SerializeField] private float perCharacterDelay;
    [SerializeField] private Color visibleColor;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject continueArrow;
    [SerializeField] private GameObject stopSquare;
    private int characterCount;
    private string currentText;
    private bool shown;

    private void Awake()
    {
        boxImage.color = Color.clear;
        boxRt.localScale = new Vector2(shrinkScaleX, shrinkScaleY);
        text.gameObject.SetActive(false);
        shown = false;
    }

    public void Print(string msg)
    {
        if (!shown) Show();
        characterCount = 0;
        currentText = msg;
        text.text = "";
        stopSquare.SetActive(false);
        continueArrow.SetActive(false);
        StartCoroutine(CO_DelayedPrintStart());
    }

    [Button]
    public void Show()
    {
        boxRt.DOScale(Vector2.one, 0.2f);
        boxImage.DOColor(visibleColor, 0.2f);
        text.gameObject.SetActive(true);
        shown = true;
    }

    [Button]
    public void Hide()
    {
        boxRt.DOScale(new Vector2(shrinkScaleX, shrinkScaleY), 0.2f);
        boxImage.DOColor(Color.clear, 0.2f);
        text.gameObject.SetActive(false);
        shown = false;
        stopSquare.SetActive(false);
        continueArrow.SetActive(false);
    }

    private IEnumerator CO_DelayedPrintStart()
    {
        yield return new WaitForSeconds(printTextDelay);
        StartCoroutine(CO_Print());
    }

    private IEnumerator CO_Print()
    {
        while (characterCount < currentText.Length)
        {
            text.text += currentText[characterCount];
            characterCount++;
            yield return new WaitForSeconds(perCharacterDelay);
        }

        if (dialogueManager.AnyFurtherNodes()) continueArrow.SetActive(true);
        else stopSquare.SetActive(true);
    }

    public bool FinishedPrinting()
    {
        return characterCount >= currentText.Length;
    }
}
