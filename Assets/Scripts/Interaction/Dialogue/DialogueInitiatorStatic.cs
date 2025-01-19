using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A non-dynamic dialogue initiator. It always initiates the same dialogue tree. Useful for things like signs that will always say the same thing.
public class DialogueInitiatorStatic : DialogueInitiator
{
    [SerializeField] private DialogueNodeSO initialNode;

    public override DialogueNodeSO ChooseStartingNode()
    {
        return initialNode;
    }
}
