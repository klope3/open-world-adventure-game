using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    [SerializeField] private Sprite heartEmpty;
    [SerializeField] private Sprite heartOneQuarter;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartThreeQuarters;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private RectTransform heartsParent;
    [SerializeField] private HealthHandler playerHealth;

    private void Awake()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (playerHealth.MaxHealth % 4 != 0)
        {
            Debug.LogError($"{playerHealth.MaxHealth} is not a multiple of 4!");
            return;
        }

        for (int i = 0; i < heartsParent.childCount; i++)
        {
            RectTransform rt = heartsParent.GetChild(i) as RectTransform;
            rt.gameObject.SetActive(i < playerHealth.MaxHealth / 4);

            Image image = rt.GetComponent<Image>();

            int diff = (i + 1) * 4 - playerHealth.CurHealth;

            if (diff >= 4) image.sprite = heartEmpty;
            else if (diff == 3) image.sprite = heartOneQuarter;
            else if (diff == 2) image.sprite = heartHalf;
            else if (diff == 1) image.sprite = heartThreeQuarters;
            else image.sprite = heartFull;
        }
    }
}
