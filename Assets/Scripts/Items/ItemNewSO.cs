using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemNewSO", menuName = "Scriptable Objects/Items/ItemNewSO")]
public class ItemNewSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public GameObject PrettyPrefab { get; private set; } //the "pretty" 3D version of the item, shown in shops, when found in chests, etc.
    [field: SerializeField] public ItemBehaviorSO[] ItemBehaviors { get; private set; }
}
