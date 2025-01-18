using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/DialogueNodeSO")]
public class DialogueNodeSO : ScriptableObject
{
    [SerializeField] private TextAsset textAsset;
    [SerializeField] private DialogueNodeSO nextNode;

    public string Text
    {
        get
        {
            return textAsset.text;
        }
    }
    public DialogueNodeSO NextNode
    {
        get
        {
            return nextNode;
        }
    }
}
