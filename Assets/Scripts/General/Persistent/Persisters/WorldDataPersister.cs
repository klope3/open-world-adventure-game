using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDataPersister : MonoBehaviour
{
    [SerializeField] private GameClock gameClock;

    private void Update()
    {
        PersistentGameData.SaveData.WorldData.timeOfDay = gameClock.DayTimer;
        PersistentGameData.SaveData.WorldData.days = gameClock.DaysElapsed;
    }
}
