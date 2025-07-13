using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNodeSO", menuName = "Scriptable Objects/Conditions/CheckTimeOfDaySO")]
public class CheckTimeOfDaySO : ScriptableObject, ICondition
{
    [SerializeField] private GameClock.TimeOfDay requiredTimeOfDay;

    public bool Evaluate()
    {
        //GameClock gameClock = GameObject.FindGameObjectWithTag("GameClock").GetComponent<GameClock>();
        //WeatherManager weatherManager = GameObject.FindGameObjectWithTag("Environment").GetComponent<WeatherManager>();
        //return gameClock.CurrentTimeOfDay == requiredTimeOfDay;
        return false; //the dialogue system needs to be reworked anyway
    }
}
