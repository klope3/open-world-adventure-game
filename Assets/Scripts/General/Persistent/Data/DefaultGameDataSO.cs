using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultGameDataSO", menuName = "Scriptable Objects/DefaultGameDataSO")]
public class DefaultGameDataSO : ScriptableObject
{
    [field: SerializeField] public int PlayerHealth;
    [field: SerializeField] public int PlayerHealthMax;
    [field: SerializeField] public int PlayerMoney;

    public PlayerData GetPlayerData()
    {
        PlayerData playerData = new PlayerData();

        playerData.health = PlayerHealth;
        playerData.healthMax = PlayerHealthMax;
        playerData.money = PlayerMoney;

        return playerData;
    }
}
