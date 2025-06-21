using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class Lootable : MonoBehaviour, IInteractable
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip lootingAnimation;
    [SerializeField] private Transform openFromLocation;
    private bool looted;
    public UnityEngine.Events.UnityEvent OnAnimationStart;
    [field: SerializeField] public ItemSO ItemSO { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
   
    public Transform OpenFromLocation
    {
        get
        {
            return openFromLocation;
        }
    }

    public void DoInteraction(PlayerInteractionHandler interactionHandler)
    {
        if (looted) return;

        looted = true;
        interactionHandler.DoLootingSequence(this);
    }

    public void DoLootingAnimation()
    {
        animancer.Play(lootingAnimation);
        OnAnimationStart?.Invoke();
    }

    public string GetInteractionName()
    {
        return "Open";
    }
}
