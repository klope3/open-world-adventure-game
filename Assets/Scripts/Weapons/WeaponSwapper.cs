using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Swaps the player's primary and secondary weapons. The primary weapon is the one that will be used as soon as the player gives an "attack" input.
//Will eventually need to account for any possible combination of primary/secondary weapons.
public class WeaponSwapper : MonoBehaviour
{
    [SerializeField] private PlayerMeleeAttackModule melee;
    [SerializeField] private PlayerBowAttackModule bow;
    [SerializeField] private GameObject bowVisual;
    [SerializeField] private GameObject meleeVisual;
    public System.Action OnSwap;

    private void OnEnable()
    {
        InputActionsProvider.OnSwapButtonStarted += InputActionsProvider_OnSwapButtonStarted;
    }

    private void OnDisable()
    {
        InputActionsProvider.OnSwapButtonStarted -= InputActionsProvider_OnSwapButtonStarted;
    }

    private void InputActionsProvider_OnSwapButtonStarted()
    {
        Swap();
    }

    [Sirenix.OdinInspector.Button]
    public void Swap()
    {
        melee.enabled = !melee.enabled;
        bow.enabled = !bow.enabled;

        meleeVisual.SetActive(melee.enabled);
        bowVisual.SetActive(bow.enabled);

        OnSwap?.Invoke();
    }
}
