using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionZone : MonoBehaviour
{
    private List<IInteractable> interactables;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
        interactables = new List<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        interactables.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        interactables.Remove(interactable);
    }

    public IInteractable GetPrioritizedInteractable()
    {
        //unimplemented. Will eventually calculate which interactable should be prioritized currently, 
        //based on distance, how directly the player's facing it, etc.
        //another script can retrieve this prioritized interactable and actually interact with it.
        return null;
    }

    //the InteractionZone's job should be to keep track of what's inside and outside the zone,
    //not actually interact with any of it.
    //public void Interact()
    //{
    //    if (interactables.Count == 0) return;
    //
    //    IInteractable firstInteractable = interactables[0];
    //    firstInteractable.DoInteraction();
    //}
}
