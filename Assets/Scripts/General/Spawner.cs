using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObjectPool pool;
    [field: SerializeField, Tooltip("Used for when the pool reference is set dynamically.")] 
    public GameObject RequestedPrefab { get; private set; }
    [SerializeField, Min(0.001f)] private float spawnsPerSecond;
    [SerializeField, Min(0.001f)] private float minSpawnRadius;
    [SerializeField, Min(0.001f)] private float maxSpawnRadius;
    [SerializeField] private bool use2DRadius;
    [SerializeField, Min(1)] private int maxSpawnedCount;
    [SerializeField, Tooltip("If the spawner spawns NPCs, it will register/unregister spawned NPCs wih the NonPlayerCharacterManager.")] 
    private SpawnerType spawnerType;
    [field: SerializeField] private NonPlayerCharacterManager npcManager;
    private float spawnTimer;
    private int spawnedCount;
    [ShowInInspector, DisplayAsString]
    public float SpawnedCount
    {
        get
        {
            return spawnedCount;
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
        spawnable.Initialize();
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
        if (spawnerType == SpawnerType.NPC_Enemy) npcManager.RegisterEnemy(npcBase);
    }

    public void SetGameObjectPool(GameObjectPool pool)
    {
        this.pool = pool;
    }

    public void SetNPCManager(NonPlayerCharacterManager npcManager)
    {
        this.npcManager = npcManager;
    }

    private void Spawnable_OnDie(Spawnable spawnable)
    {
        spawnable.OnDie -= Spawnable_OnDie;
        spawnedCount--;
    }
}
