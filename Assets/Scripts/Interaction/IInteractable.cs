using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void DoInteraction(PlayerInteractionHandler interactionHandler);
    public string GetInteractionName(); //"Climb", "Read", "Speak", "Open", etc.
}
