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
        Debug.Log($"Added {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        interactables.Remove(interactable);
        Debug.Log($"Removed {other.name}");
    }

    public void Interact()
    {
        if (interactables.Count == 0) return;

        IInteractable firstInteractable = interactables[0];
        firstInteractable.DoInteraction();
    }
}
