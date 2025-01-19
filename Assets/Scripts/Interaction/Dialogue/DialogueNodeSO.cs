using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/DialogueNodeSO")]
public class DialogueNodeSO : SerializedScriptableObject
{
    [SerializeField, TextArea(7, 7)] private string text;
    [SerializeField] private DialogueNodeWithCondition[] nextNodeOptions;
    [SerializeField] private string[] responseChoices;
    [SerializeField, Tooltip("If true, the DialogueManager will NOT print this node and will instead use its ChooseNextNode() method to proceed immediately. Useful for conditional dialogue beginnings.")] 
    private bool passthru;
    
    public string[] ResponseChoices
    {
        get
        {
            return responseChoices;
        }
    }
    public bool Passthru
    {
        get
        {
            return passthru;
        }
    }

    public string Text
    {
        get
        {
            return text;
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

    public bool HasChoices()
    {
        if (responseChoices == null) return false;
        return responseChoices.Length > 0;
    }
}
