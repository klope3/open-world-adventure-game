using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NonPlayerCharacterBase : MonoBehaviour
{
    [SerializeField] private StateManagerBase stateManager;
    [SerializeField] private Animator animator;
    [SerializeField] private ECM2.Character character;

    [Button]
    public void SetFrozen(bool frozen)
    {
        stateManager.enabled = !frozen;
        animator.enabled = !frozen;
        character.enabled = !frozen;
    }
}
