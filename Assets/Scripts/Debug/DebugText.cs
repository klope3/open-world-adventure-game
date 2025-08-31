using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private MonoBehaviour textProvider;

    private void OnValidate()
    {
        if (textProvider != null)
        {
            IDebugTextProvider debugTextProvider = textProvider.GetComponent<IDebugTextProvider>();
            if (debugTextProvider == null)
            {
                textProvider = null;
                Debug.LogError("textProvider must implement IDebugTextProvider");
            }
        }
    }

    private void Update()
    {
        if (text == null || textProvider == null) return;

        text.text = textProvider.GetComponent<IDebugTextProvider>().GetDebugText();
    }
}
