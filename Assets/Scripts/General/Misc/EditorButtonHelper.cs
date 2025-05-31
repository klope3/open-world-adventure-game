using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class EditorButtonHelper : MonoBehaviour
{
    public UnityEvent OnButtonPressed;

    [Button, InfoBox("Makes it easy to press a button in-editor and have multiple things happen at once via an event.")]
    public void Execute()
    {
        OnButtonPressed?.Invoke();
    }
}
