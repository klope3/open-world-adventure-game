using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public GameObject PrettyPrefab { get; private set; } //the "pretty" 3D version of the item, shown in shops, when found in chests, etc.
    [field: SerializeField] public string LootingMessage { get; private set; }

}
