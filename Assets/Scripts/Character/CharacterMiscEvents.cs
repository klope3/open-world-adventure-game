using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMiscEvents : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    public UnityEvent OnLanded;

    private void Awake()
    {
        character.Landed += Character_Landed;
    }

    private void Character_Landed(Vector3 landingVelocity)
    {
        OnLanded?.Invoke();
    }
}
