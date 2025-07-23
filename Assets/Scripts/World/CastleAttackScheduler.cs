using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAttackScheduler : MonoBehaviour
{
    [SerializeField] private GameClock gameClock;
    [SerializeField] private Spawner spawner;

    public void Initialize()
    {
        gameClock.ScheduleClockAction(new GameClockAction(GameClock.DAY_START, () => spawner.enabled = false));
        gameClock.ScheduleClockAction(new GameClockAction(GameClock.NIGHT_START, () => spawner.enabled = true));
    }
}
