using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameClock : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 0.00278f;
    [ShowInInspector, DisplayAsString] private float dayTimer;
    [ShowInInspector, DisplayAsString] private int daysElapsed;
    [ShowInInspector, DisplayAsString]
    public TimeOfDay CurrentTimeOfDay
    {
        get
        {
            if (dayTimer >= MORNING_START && dayTimer < DAY_START) return TimeOfDay.Morning;
            if (dayTimer >= DAY_START && dayTimer < EVENING_START) return TimeOfDay.Day;
            if (dayTimer >= EVENING_START && dayTimer < NIGHT_START) return TimeOfDay.Evening;
            return TimeOfDay.Night;
        }
    }
    public float DayTimer
    {
        get
        {
            return dayTimer;
        }
    }
    public int DaysElapsed
    {
        get
        {
            return daysElapsed;
        }
    }

    public static readonly float MORNING_START = 0;
    public static readonly float DAY_START = 0.0625f;
    public static readonly float EVENING_START = 0.4375f;
    public static readonly float NIGHT_START = 0.5f;

    public enum TimeOfDay
    {
        Morning,
        Day,
        Evening,
        Night
    }

    private void Update()
    {
        dayTimer += Time.deltaTime * timeMultiplier;
        if (dayTimer > 1)
        {
            dayTimer = 0;
            daysElapsed++;
        }
    }

    [Button]
    public void SetTime(TimeOfDay timeOfDay)
    {
        if (timeOfDay == TimeOfDay.Morning)
        {
            dayTimer = MORNING_START;
        }
        if (timeOfDay == TimeOfDay.Day)
        {
            dayTimer = DAY_START;
        }
        if (timeOfDay == TimeOfDay.Evening)
        {
            dayTimer = EVENING_START;
        }
        if (timeOfDay == TimeOfDay.Night)
        {
            dayTimer = NIGHT_START;
        }
    }
}
