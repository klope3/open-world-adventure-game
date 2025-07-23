using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameClock : MonoBehaviour
{
    [SerializeField] private float timeMultiplier = 0.00278f;
    [ShowInInspector, DisplayAsString] private float dayTimer;
    [ShowInInspector, DisplayAsString] private int daysElapsed;
    private List<GameClockAction> clockActions; //these callbacks will be called at (approximately) the specified times of day
    
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

    public void Initialize()
    {
        clockActions = new List<GameClockAction>();
    }

    private void Update()
    {
        dayTimer += Time.deltaTime * timeMultiplier;
        if (dayTimer > 1)
        {
            dayTimer = 0;
            daysElapsed++;
            ResetClockActions();
        }

        GameClockAction actionToCall = clockActions.Find(a => !a.actionCalled && a.Time < dayTimer);
        if (actionToCall != null && !actionToCall.actionCalled)
        {
            actionToCall.Action();
            actionToCall.actionCalled = true;
        }
    }

    private void ResetClockActions()
    {
        foreach (GameClockAction action in clockActions)
        {
            action.actionCalled = false;
        }
    }

    public void ScheduleClockAction(GameClockAction action)
    {
        GameClockAction existingAction = clockActions.Find(a => a.Time == action.Time);
        if (existingAction != null) Debug.LogError($"There is already an action scheduled at time = {action.Time}. Multiple actions at the same time not currently supported.");
        clockActions.Add(action);
        clockActions.Sort((a, b) => a.Time.CompareTo(b.Time));
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