using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClockAction
{
    public float Time { get; private set; }
    public System.Action Action { get; private set; }
    public bool actionCalled; //has the action been called once today?

    public GameClockAction(float time, System.Action action)
    {
        Time = time;
        Action = action;
    }
}
