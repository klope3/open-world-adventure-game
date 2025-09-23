using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemInventory : MonoBehaviour
{
    private List<ItemInventoryEntry> _i; //items; should only be accessed via Items property
    [ShowInInspector, ReadOnly] private List<ItemInventoryEntry> Items
    {
        get
        {
            if (_i == null)
            {
                _i = new List<ItemInventoryEntry>();
            }
            return _i;
        }
    }
    public System.Action OnChanged;

    [Button]
    public void Add(ItemNewSO item, int quantity)
    {
        if (item == null || quantity <= 0) return;

        int index = Items.FindIndex(entry => entry != null && entry.Item == item);
        if (index != -1)
        {
            Items[index].Quantity += quantity;
            OnChanged?.Invoke();
            return;
        }

        int firstNullIndex = Items.FindIndex(entry => entry == null);
        if (firstNullIndex != -1)
        {
            //null "gaps" are allowed to help preserve layout in grid-based inventory views. prioritize filling these before adding a new entry to the list.
            Items[firstNullIndex] = new ItemInventoryEntry(item, quantity);
            OnChanged?.Invoke();
            return;
        }

        Items.Add(new ItemInventoryEntry(item, quantity));
        OnChanged?.Invoke();
    }

    /// <summary>
    /// Remove a quantity of a specific item from the inventory.
    /// </summary>
    /// <param name="item">The Item to add.</param>
    /// <param name="quantity">The quantity of the item to add.</param>
    /// <param name="removeEmptyInnersFromList">If removing the given quantity reduces an entry to quantity 0, set the entry to null instead of removing it from the list. This helps preserve layout in grid-based inventory views. Note that for entries at the end of the list, this parameter is ignored, and the entry is always removed.</param>
    [Button]
    public void Remove(ItemNewSO item, int quantity, bool removeEmptyFromList = false)
    {
        if (item == null || quantity <= 0) return;

        int index = Items.FindIndex(entry => entry != null && entry.Item == item);
        if (index == -1)
        {
            Debug.Log($"Item '{item.Name}' not found in inventory.");
            return;
        }

        Items[index].Quantity -= quantity;

        if (Items[index].Quantity <= 0)
        {
            Items[index] = null;
            Cleanup();
            //int lastNonNullIndex = Items.FindLastIndex(entry => entry != null);
            //Debug.Log($"index = {index}, last non null index = {lastNonNullIndex}");
            //if (removeEmptyFromList || index >= lastNonNullIndex) Items.Remove(Items[index]);
            //allow null "gaps" in the inventory list so that grid-based inventory views can retain the same layout even when an item entry is depleted.
            //else Items[index] = null;
        }

        OnChanged?.Invoke();
    }

    private void Cleanup()
    {
        int lastNonNullIndex = Items.FindLastIndex(entry => entry != null);
        //trim null entries from end of list, remove null entries from within list if requested, etc.
    }

    /// <summary>
    /// Get a shallow copy of the items list. The real list is kept internal and not exposed to the outside.
    /// </summary>
    /// <returns></returns>
    public List<ItemInventoryEntry> GetClonedList()
    {
        return new List<ItemInventoryEntry>(Items);
    }
}
