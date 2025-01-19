using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public abstract class PlayerState : State
{
    protected PlayerStateManager stateManager;
    protected Character character;
    protected ECM2CharacterAdapter characterAdapter;

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter)
    {
        this.stateManager = stateManager;
        this.characterAdapter = characterAdapter;
        this.character = character;
    }

    public abstract void PostInitialize();
}
