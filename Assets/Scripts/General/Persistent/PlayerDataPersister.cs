using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//watches for changes in player-related data and updates PersistentGameData when needed
public class PlayerDataPersister : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth;
    [SerializeField] private MoneyHandler moneyHandler;

    public void Initialize()
    {
        playerHealth.OnDamaged += PlayerHealth_OnDamaged;
        playerHealth.OnHealed += PlayerHealth_OnHealed;
        playerHealth.OnDied += PlayerHealth_OnDied;

        moneyHandler.OnMoneyChanged += MoneyHandler_OnMoneyChanged;
    }

    public void OnDisable()
    {
        playerHealth.OnDamaged -= PlayerHealth_OnDamaged;
        playerHealth.OnHealed -= PlayerHealth_OnHealed;
        playerHealth.OnDied -= PlayerHealth_OnDied;

        moneyHandler.OnMoneyChanged -= MoneyHandler_OnMoneyChanged;
    }

    private void MoneyHandler_OnMoneyChanged()
    {
        PersistentGameData.playerMoney = moneyHandler.Money;
    }

    private void PlayerHealth_OnDamaged(Vector3 position)
    {
        PersistentGameData.playerHealth = playerHealth.CurHealth;
    }

    private void PlayerHealth_OnHealed()
    {
        PersistentGameData.playerHealth = playerHealth.CurHealth;
    }

    private void PlayerHealth_OnDied()
    {
        PersistentGameData.playerHealth = playerHealth.CurHealth;
    }
}
