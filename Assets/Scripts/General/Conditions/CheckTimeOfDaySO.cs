using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/Conditions/CheckTimeOfDaySO")]
public class CheckTimeOfDaySO : ScriptableObject, ICondition
{
    [SerializeField] private WeatherManager.TimeOfDay requiredTimeOfDay;

    public bool Evaluate()
    {
        WeatherManager weatherManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<WeatherManager>();
        return weatherManager.CurrentTimeOfDay == requiredTimeOfDay;
    }
}
