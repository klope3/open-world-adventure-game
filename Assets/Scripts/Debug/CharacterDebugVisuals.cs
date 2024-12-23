using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class CharacterDebugVisuals : MonoBehaviour
{
    [SerializeField] private GameObject groundedVisual;
    [SerializeField] private Character character;

    private void Update()
    {
        groundedVisual.SetActive(character.IsGrounded());
    }
}
