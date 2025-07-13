using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NonPlayerCharacterManager : MonoBehaviour
{
    private List<NonPlayerCharacterBase> enemies;
    private List<NonPlayerCharacterBase> neutralNpcs;
    [ShowInInspector, DisplayAsString] 
    public int RegisteredEnemies
    {
        get
        {
            return enemies == null ? 0 : enemies.Count;
        }
    }
    [ShowInInspector, DisplayAsString]
    public int RegisteredNeutralNpcs
    {
        get
        {
            return neutralNpcs == null ? 0 : neutralNpcs.Count;
        }
    }

    public void Initialize()
    {
        enemies = new List<NonPlayerCharacterBase>();
        neutralNpcs = new List<NonPlayerCharacterBase>();
    }

    public void RegisterEnemy(NonPlayerCharacterBase npc)
    {
        enemies.Add(npc);
    }

    public void UnregisterEnemy(NonPlayerCharacterBase npc)
    {
        enemies.Remove(npc);
    }

    public void RegisterNeutralNPC(NonPlayerCharacterBase npc)
    {
        neutralNpcs.Add(npc);
    }

    public void UnregisterNeutralNPC(NonPlayerCharacterBase npc)
    {
        neutralNpcs.Remove(npc);
    }

    [Button]
    public void SetEnemiesFrozen(bool frozen)
    {
        foreach (NonPlayerCharacterBase npc in enemies)
        {
            npc.SetFrozen(frozen);
        }
    }
}
