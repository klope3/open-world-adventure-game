using DunGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1Finalizer : DungeonFinalizer
{
    [SerializeField] private GameObjectPool skeletonPool;
    [SerializeField] private GameObjectPool flowerSpiderPool;
    [SerializeField] private NonPlayerCharacterManager npcManager;

    protected override void FinalizeDungeon(DungeonGenerator generator)
    {
        foreach (Tile tile in generator.CurrentDungeon.AllTiles)
        {
            Dungeon1TileFinalizer tileFinalizer = tile.GetComponent<Dungeon1TileFinalizer>();
            if (tileFinalizer == null) continue;

            foreach (Spawner s in tileFinalizer.FlowerSpiderSpawners)
            {
                s.SetGameObjectPool(flowerSpiderPool);
                s.SetNPCManager(npcManager);
            }
            foreach (Spawner s in tileFinalizer.SkeletonSpawners)
            {
                s.SetGameObjectPool(skeletonPool);
                s.SetNPCManager(npcManager);
            }
        }
    }
}
