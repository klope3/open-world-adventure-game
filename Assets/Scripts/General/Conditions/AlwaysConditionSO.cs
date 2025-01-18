using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/Conditions/AlwaysConditionSO")]
public class AlwaysConditionSO : ScriptableObject, ICondition
{
    public bool Evaluate()
    {
        return true;
    }
}
