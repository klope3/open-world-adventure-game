using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlotButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [field: SerializeField] public Button Button { get; private set; }

    public void SetText(string text)
    {
        nameText.text = text;
    }
}
