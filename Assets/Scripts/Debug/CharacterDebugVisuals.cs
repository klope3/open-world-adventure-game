using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using TMPro;
using Sirenix.OdinInspector;

public class CharacterDebugVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerStateText;
    [SerializeField] private GameObject groundedVisual;
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private GameObject moveInputArrow;

    private void Update()
    {
        groundedVisual.SetActive(character.IsGrounded());
        playerStateText.text = playerStateManager.CurrentState.GetDebugName();
        UpdateInputVector();
    }

    private void UpdateInputVector()
    {
        Vector3 startPoint = transform.position + Vector3.up;
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        inputVec = new Vector3(inputVec.x, 0, inputVec.y);
        inputVec = inputVec.relativeTo(character.cameraTransform);
        moveInputArrow.transform.forward = inputVec.magnitude < 0.05f? Vector3.forward : inputVec;
        moveInputArrow.transform.localScale = new Vector3(1, 1, inputVec.magnitude);
    }
}
