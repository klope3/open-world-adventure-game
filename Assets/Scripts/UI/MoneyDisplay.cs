using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField, Tooltip("The displayed digits will increment toward the player's actual money amount at this rate per second.")] 
    private float lerpRate;
    private float lerpedMoney; //this increases/decreases toward the player's actual money amount, and is the actual value displayed onscreen
    public UnityEvent OnIncrement;

    public void Initialize()
    {
        lerpedMoney = moneyHandler.Money;
        moneyText.text = BuildString(moneyHandler.Money);
    }

    private void Update()
    {
        int prevRoundedLerped = Mathf.RoundToInt(lerpedMoney);
        if (prevRoundedLerped == moneyHandler.Money) return;

        int increment = moneyHandler.Money > lerpedMoney ? 1 : -1;
        lerpedMoney += Time.deltaTime * lerpRate * increment;
        int newRoundedLerped = Mathf.RoundToInt(lerpedMoney);

        moneyText.text = BuildString(newRoundedLerped);

        if (prevRoundedLerped != newRoundedLerped) OnIncrement?.Invoke();
    }

    private string BuildString(int amountToDisplay)
    {
        int totalDigitCount = $"{moneyHandler.MaxMoney}".Length;
        int moneyDigitCount = $"{amountToDisplay}".Length;
        int zerosCount = totalDigitCount - moneyDigitCount;
        string zeros = new string('0', zerosCount);
        
        return $"{zeros}{amountToDisplay}";
    }
}
