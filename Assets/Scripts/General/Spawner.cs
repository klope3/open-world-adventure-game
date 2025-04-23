using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool pool;
    [SerializeField, Min(0.001f)] private float spawnsPerSecond;
    [SerializeField, Min(0.001f)] private float minSpawnRadius;
    [SerializeField, Min(0.001f)] private float maxSpawnRadius;
    [SerializeField] private bool use2DRadius;
    [SerializeField, Min(1)] private int maxSpawnedCount;
    [SerializeField, Tooltip("If the spawner spawns NPCs, it will register/unregister spawned NPCs wih the NonPlayerCharacterManager.")] 
    private SpawnerType spawnerType;
    private NonPlayerCharacterManager npcm; //NPC Manager; lazy initialized
    private float spawnTimer;
    private int spawnedCount;

    private NonPlayerCharacterManager NpcManager
    {
        get
        {
            if (npcm == null) npcm = FindObjectOfType<NonPlayerCharacterManager>();
            if (npcm == null) Debug.LogError("No NPC Manager found");
            return npcm;
        }
    }

    public enum SpawnerType
    {
        Other,
        NPC_Enemy,
        NPC_Neutral,
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > 1 / spawnsPerSecond)
        {
            spawnTimer = 0;
            if (spawnedCount < maxSpawnedCount) Spawn();
        }
    }

    private void Spawn()
    {
        if (minSpawnRadius > maxSpawnRadius)
        {
            Debug.LogWarning("This spawner's min radius is greater than its max radius!");
        }

        GameObject go = pool.GetPooledObject();
        Vector3 randPos = transform.position + Random.insideUnitSphere.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        if (use2DRadius) randPos.y = transform.position.y;
        go.transform.position = randPos;
        go.SetActive(true);
        Spawnable spawnable = go.GetComponent<Spawnable>();
        if (spawnable == null)
        {
            Debug.LogWarning("Spawned an object with no Spawnable component!");
            return;
        }

        spawnable.originSpawner = this;
        spawnable.OnDie += Spawnable_OnDie;
        spawnedCount++;

        RegisterSpawnedNPC(go);
    }

    private void RegisterSpawnedNPC(GameObject spawnedGo)
    {
        if (spawnerType == SpawnerType.Other) return;

        NonPlayerCharacterBase npcBase = spawnedGo.GetComponent<NonPlayerCharacterBase>();
        if (npcBase == null)
        {
            Debug.LogWarning("Tried to register an NPC with no NPC Base");
            return;
        }
        if (spawnerType == SpawnerType.NPC_Enemy) NpcManager.RegisterEnemy(npcBase);
    }
    
    private void Spawnable_OnDie(Spawnable spawnable)
    {
        spawnable.OnDie -= Spawnable_OnDie;
        spawnedCount--;
    }
}
