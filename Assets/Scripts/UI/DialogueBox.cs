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
    [SerializeField] private TextMeshProUGUI dialogueChoice0Text;
    [SerializeField] private TextMeshProUGUI dialogueChoice1Text;
    [SerializeField] private GameObject dialogueChoice0Indicator;
    [SerializeField] private GameObject dialogueChoice1Indicator;
    private int characterCount;
    private string currentText;
    private bool shown;

    private void Awake()
    {
        boxImage.color = Color.clear;
        boxRt.localScale = new Vector2(shrinkScaleX, shrinkScaleY);
        text.gameObject.SetActive(false);
        shown = false;
        dialogueChoice0Indicator.SetActive(false);
        dialogueChoice1Indicator.SetActive(false);
        dialogueChoice0Text.gameObject.SetActive(false);
        dialogueChoice1Text.gameObject.SetActive(false);
    }

    public void Print(string msg, bool messageHasChoices)
    {
        if (!shown) Show();
        text.verticalAlignment = messageHasChoices ? VerticalAlignmentOptions.Top : VerticalAlignmentOptions.Middle;
        characterCount = 0;
        currentText = msg;
        text.text = "";
        stopSquare.SetActive(false);
        continueArrow.SetActive(false);
        dialogueChoice0Indicator.SetActive(false);
        dialogueChoice1Indicator.SetActive(false);
        dialogueChoice0Text.gameObject.SetActive(false);
        dialogueChoice1Text.gameObject.SetActive(false);
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
        dialogueChoice0Indicator.SetActive(false);
        dialogueChoice1Indicator.SetActive(false);
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

        string[] responseChoices = dialogueManager.GetDialogueChoices();
        if (responseChoices.Length > 0)
        {
            dialogueChoice0Text.text = responseChoices[0];
            dialogueChoice0Text.gameObject.SetActive(true);

            if (responseChoices.Length > 1)
            {
                dialogueChoice1Text.text = responseChoices[1];
                dialogueChoice1Text.gameObject.SetActive(true);
            }

            UpdateChoiceIndicators();
        }
        else if (dialogueManager.AnyFurtherNodes()) continueArrow.SetActive(true);
        else stopSquare.SetActive(true);
    }

    public void UpdateChoiceIndicators()
    {
        dialogueChoice0Indicator.SetActive(dialogueManager.SelectedChoiceIndex == 0);
        dialogueChoice1Indicator.SetActive(dialogueManager.SelectedChoiceIndex == 1);
    }

    public bool FinishedPrinting()
    {
        return characterCount >= currentText.Length;
    }
}
