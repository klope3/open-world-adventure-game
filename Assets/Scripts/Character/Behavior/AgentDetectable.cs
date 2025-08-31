using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDetectable : MonoBehaviour, IAgentDetectable
{
    [SerializeField] public bool detectable;

    //could be overridden for special detection behavior
    public bool IsDetectable(GameObject observer)
    {
        return detectable;
    }
}
