using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private RaycastChecker climbingDetector;
    [SerializeField] private GameObjectDetectorZone detectorZone;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerChestAnimationEvents chestAnimationEvents;
    [SerializeField] private AudioSource rewardSmallSound;
    [SerializeField] private Transform lootingObjectAnchor;
    [SerializeField] private DialogueBox dialogueBox;
    [SerializeField] private MoneyHandler moneyHandler;

    private Lootable currentLootable; //when looting sequence starts, the lootable being used is stored here for reference during the sequence
    private GameObject lootingDisplayObject;

    private void Awake()
    {
        InputActionsProvider.OnInteractButtonStarted += Interact_started;
        chestAnimationEvents.ChestSmall_OnHandsRaised += ChestSmall_OnHandsRaised;
        chestAnimationEvents.ChestSmall_OnKicked += ChestSmall_OnKicked;
        chestAnimationEvents.ChestSmall_OnCameraZoomStart += ChestSmall_OnCameraZoomStart;
    }

    private void ChestSmall_OnCameraZoomStart()
    {
        cameraController.SetActiveCamera(CameraController.ActiveCamera.Looting);
    }

    private void ChestSmall_OnKicked()
    {
        currentLootable.DoLootingAnimation();
    }

    private void ChestSmall_OnHandsRaised()
    {
        dialogueBox.Print($"You got {currentLootable.ItemSO.ItemName}! {currentLootable.ItemSO.LootingMessage}", false);
        if (currentLootable.ItemSO.GiveMoneyAmount > 0) moneyHandler.AddMoney(currentLootable.ItemSO.GiveMoneyAmount);
        rewardSmallSound.Play();
        lootingDisplayObject = Instantiate(currentLootable.ItemSO.PrettyPrefab, lootingObjectAnchor);
    }

    private void Interact_started()
    {
        //all of these checks are a sign this needs refactoring
        if (playerStateManager.CurrentStateKey == PlayerStateManager.LOOT_STATE)
        {
            playerStateManager.trigger = PlayerStateManager.DEFAULT_STATE;
            gameStateManager.trigger = GameStateManager.DEFAULT_STATE;
            dialogueBox.Hide();
            Destroy(lootingDisplayObject);
            return;
        }

        if (playerStateManager.CurrentStateKey == PlayerStateManager.CLIMBING_STATE)
        {
            playerStateManager.trigger = PlayerStateManager.FALLING_STATE;
            return;
        }

        if (climbingDetector.Check()) //climbables are not currently IInteractable because they work with raycast detection, not GameObjectDetetorZones
        {
            playerStateManager.trigger = PlayerStateManager.CLIMBING_START_STATE;
            return;
        }

        List<GameObject> detectedObjects = detectorZone.GetObjectsList();
        foreach (GameObject obj in detectedObjects) //currently just interacts with the first interactable it finds; there should be some consistent prioritization logic in case multiple are found
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.DoInteraction(this);
                return;
            }
        }
    }

    public void DoLootingSequence(Lootable lootable) //there may be slightly different behavior based on which kind of lootable is being used
    {
        playerStateManager.trigger = PlayerStateManager.LOOT_STATE;
        gameStateManager.trigger = GameStateManager.LOOT_STATE;
        character.TeleportPosition(lootable.OpenFromLocation.position);
        character.TeleportRotation(lootable.OpenFromLocation.rotation);
        currentLootable = lootable;
    }
}
