using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InventoryList : MonoBehaviour
{
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private InventoryListItem listItemPrefab;
    [SerializeField] private RectTransform gridParent;

    private void OnEnable()
    {
        UpdateList();
        inventory.OnChanged += UpdateList;
    }

    private void OnDisable()
    {
        inventory.OnChanged -= UpdateList;
    }

    [Button]
    public void UpdateList()
    {
        List<ItemInventoryEntry> entries = inventory.GetClonedList();
        for (int i = 0; i < entries.Count; i++)
        {
            ItemInventoryEntry entry = entries[i];
            if (i >= gridParent.childCount)
            {
                InventoryListItem newListItem = Instantiate(listItemPrefab, gridParent);
                newListItem.UpdateItem(entry);
                continue;
            }
            else
            {
                Transform child = gridParent.GetChild(i);
                InventoryListItem listItem = child.GetComponent<InventoryListItem>();
                listItem.UpdateItem(entry);
            }
        }
        TrimEnd(entries);
    }

    private void TrimEnd(List<ItemInventoryEntry> entries)
    {
        if (gridParent.childCount > entries.Count)
        {
            List<GameObject> objectsToDestroy = new List<GameObject>();
            for (int i = gridParent.childCount - 1; i > entries.Count; i--)
            {
                objectsToDestroy.Add(gridParent.GetChild(i).gameObject);
            }
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}
