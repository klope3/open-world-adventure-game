using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition
{
    public string targetStateName;
    public delegate bool BoolFunc();
    public BoolFunc transitionCondition;

    public StateTransition(string targetStateName, BoolFunc transitionCondition)
    {
        this.targetStateName = targetStateName;
        this.transitionCondition = transitionCondition;
    }
}
