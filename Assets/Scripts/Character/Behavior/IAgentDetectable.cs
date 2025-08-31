using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentDetectable
{
    public bool IsDetectable(GameObject observer);
}
