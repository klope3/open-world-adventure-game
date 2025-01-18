using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeWithCondition 
{
    [SerializeField] private ICondition condition;
    [SerializeField] private DialogueNodeSO node;
    public ICondition Condition
    {
        get
        {
            return condition;
        }
    }
    public DialogueNodeSO Node
    {
        get
        {
            return node;
        }
    }
}
