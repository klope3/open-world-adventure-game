using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemInventoryEntry
{
    public ItemNewSO Item { get; private set; }
    [ShowInInspector] public string Name
    {
        get
        {
            return Item.Name;
        }
    }
    [ShowInInspector, DisplayAsString] public int Quantity { get; set; }

    public ItemInventoryEntry(ItemNewSO item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
