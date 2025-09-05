using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBasicBehaviorDataSO", menuName = "Scriptable Objects/Enemy/EnemyBasicBehaviorDataSO")]
public class EnemyBasicBehaviorDataSO : ScriptableObject
{
    [field: SerializeField] public float WanderTime { get; private set; }
    [field: SerializeField] public float WanderSpeed { get; private set; }
    [field: SerializeField] public float PauseTime { get; private set; }
    [field: SerializeField] public float ChaseSpeed { get; private set; }
    [field: SerializeField] public float AttackDuration { get; private set; }
    [field: SerializeField] public float AttackDistance { get; private set; }
    [field: SerializeField] public float AttackRecoveryTime { get; private set; }
}
