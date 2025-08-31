using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDetectorZone : GameObjectDetectorZone
{
    [SerializeField, Tooltip("The GameObject to treat as the agent doing the observing.")] private GameObject observer;

    protected override bool IsObjectValid(GameObject obj)
    {
        IAgentDetectable detectable = obj.GetComponent<IAgentDetectable>();
        return detectable == null ? false : detectable.IsDetectable(observer);
    }
}
