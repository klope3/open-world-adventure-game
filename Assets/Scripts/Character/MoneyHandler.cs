using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private int maxMoney;
    private int money;
    public UnityEvent OnMoneyChange;
    public System.Action OnMoneyChanged;

    public int Money
    {
        get
        {
            return money;
        }
    }
    public int MaxMoney
    {
        get
        {
            return maxMoney;
        }
    }

    public void Initialize(int initialAmount)
    {
        money = initialAmount;
    }

    [Button]
    public void AddMoney(int amount)
    {
        int prevMoney = money;
        money = Mathf.Clamp(money + amount, 0, maxMoney);
        if (money == prevMoney) return;

        OnMoneyChange?.Invoke();
        OnMoneyChanged?.Invoke();
    }
}
