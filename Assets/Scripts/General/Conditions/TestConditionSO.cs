using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/Conditions/TestConditionSO")]
public class TestConditionSO : ScriptableObject, ICondition
{
    [SerializeField] private WeatherManager.TimeOfDay requiredTime;

    public bool Evaluate()
    {
        WeatherManager weatherManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<WeatherManager>();
        return weatherManager.CurrentTimeOfDay == requiredTime;
    }
}
