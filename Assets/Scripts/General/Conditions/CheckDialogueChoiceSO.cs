using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/Conditions/CheckDialogueChoiceSO")]
public class CheckDialogueChoiceSO : ScriptableObject, ICondition
{
    [SerializeField] private int requiredChoiceIndex;

    public bool Evaluate()
    {
        DialogueManager dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        return dialogueManager.SelectedChoiceIndex == requiredChoiceIndex;
    }
}
