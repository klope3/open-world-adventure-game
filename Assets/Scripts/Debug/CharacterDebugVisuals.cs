using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using TMPro;

public class CharacterDebugVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerStateText;
    [SerializeField] private GameObject groundedVisual;
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;

    private void Update()
    {
        groundedVisual.SetActive(character.IsGrounded());
        playerStateText.text = playerStateManager.CurrentState.GetDebugName();
    }
}
