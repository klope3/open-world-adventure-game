using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private ECM2CharacterAdapter adapter;
    [SerializeField] private Animator animator;

    private int speedHash;

    private void Awake()
    {
        speedHash = Animator.StringToHash("Speed");
    }

    private void Update()
    {
        animator.SetFloat(speedHash, adapter.GetMovementInput().magnitude);
    }
}
