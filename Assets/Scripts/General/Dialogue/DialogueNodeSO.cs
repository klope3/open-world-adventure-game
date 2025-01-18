using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/DialogueNodeSO")]
public class DialogueNodeSO : SerializedScriptableObject
{
    [SerializeField] private TextAsset textAsset;
    [SerializeField] private DialogueNodeWithCondition[] nextNodeOptions;

    public string Text
    {
        get
        {
            return textAsset.text;
        }
    }

    public DialogueNodeSO ChooseNextNode()
    {
        if (nextNodeOptions == null || nextNodeOptions.Length == 0) return null;

        for (int i = 0; i < nextNodeOptions.Length; i++)
        {
            if (nextNodeOptions[i].Condition.Evaluate()) return nextNodeOptions[i].Node;
        }

        return null;
    }
}
