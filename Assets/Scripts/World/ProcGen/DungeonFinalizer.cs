using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen;

[RequireComponent(typeof(RuntimeDungeon))]
public abstract class DungeonFinalizer : MonoBehaviour
{
    private RuntimeDungeon runtimeDungeon;

	public void Initialize()
	{
		runtimeDungeon = GetComponent<RuntimeDungeon>();
		runtimeDungeon.Generator.OnGenerationStatusChanged += OnDungeonGenerationStatusChanged;
	}

	private void OnDestroy()
	{
		runtimeDungeon.Generator.OnGenerationStatusChanged -= OnDungeonGenerationStatusChanged;
	}

	private void OnDungeonGenerationStatusChanged(DungeonGenerator generator, GenerationStatus status)
	{
		if (status != GenerationStatus.Complete) return;
		FinalizeDungeon(generator);
	}

	protected abstract void FinalizeDungeon(DungeonGenerator generator);
}
