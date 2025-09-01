using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBasicBehaviorDataSO", menuName = "Scriptable Objects/Enemy/EnemyBasicBehaviorDataSO")]
public class EnemyBasicBehaviorDataSO : ScriptableObject
{
    [field: SerializeField] public float WanderTime;
    [field: SerializeField] public float WanderSpeed;
    [field: SerializeField] public float PauseTime;
    [field: SerializeField] public float ChaseSpeed;
    [field: SerializeField] public float AttackDuration;
    [field: SerializeField] public float AttackDistance;
    [field: SerializeField] public float AttackRecoveryTime;
}
