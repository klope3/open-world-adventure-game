using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryListItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI quantityText;

    public void UpdateItem(ItemInventoryEntry entry)
    {
        if (entry == null)
        {
            image.enabled = false;
            quantityText.enabled = false;
            return;
        }

        image.enabled = true;
        quantityText.enabled = true;
        image.sprite = entry.Item.Sprite;
        quantityText.text = $"{entry.Quantity}";
    }
}
