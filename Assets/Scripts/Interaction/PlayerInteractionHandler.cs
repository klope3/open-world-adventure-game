using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private PlayerClimbingDetector climbingDetector;
    [SerializeField] private PlayerStateManager stateManager;

    private void Awake()
    {
        InputActionsProvider.OnInteractButtonStarted += Interact_started;
    }

    private void Interact_started()
    {
        stateManager.trigger = PlayerStateManager.INTERACT_TRIGGER;

        //maybe retrieve prioritized interactable and interact with it here?
    }
}
