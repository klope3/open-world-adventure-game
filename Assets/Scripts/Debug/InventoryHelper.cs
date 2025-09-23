using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelper : MonoBehaviour
{
    [SerializeField] private ItemInventory inventory;
    [SerializeField] private ItemNewSO item1;
    [SerializeField] private ItemNewSO item2;
    [SerializeField] private ItemNewSO item3;
    [SerializeField] private ItemNewSO item4;

    private void Awake()
    {
        StartCoroutine(CO_Run());
    }

    private IEnumerator CO_Run()
    {
        yield return new WaitForSeconds(3);
        inventory.Add(item1, 6);
        inventory.Add(item2, 4);
        inventory.Add(item3, 13);
        inventory.Add(item4, 42);
    }
}
