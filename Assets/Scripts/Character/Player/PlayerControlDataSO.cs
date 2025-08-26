using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControlDataSO", menuName = "Scriptable Objects/Player/PlayerControlDataSO")]
public class PlayerControlDataSO : ScriptableObject
{
    [field: SerializeField] public float DefaultMoveSpeed { get; private set; }
    [field: SerializeField] public float BowMoveSpeed { get; private set; }
    [field: SerializeField] public float SwordSpinDuration { get; private set; }
    [field: SerializeField] public float SwordUpSlashDuration { get; private set; }
    [field: SerializeField] public float SwordDownSlashDuration { get; private set; }
    [field: SerializeField] public float SwordDownSlashBounceAmount { get; private set; }
    [field: SerializeField, Tooltip("Player must be falling for at least this long before a ledge grab is allowed.")]
    public float MinLedgeGrabFallTime { get; private set; }
    [field: SerializeField, Tooltip("Ledge grab will snap the player's position this far back from the vertical surface of the ledge.")]
    public float LedgeGrabDepthOffset { get; private set; }
    [field: SerializeField, Tooltip("Ledge grab will snap the player's position this far down from the horizontal surface of the ledge.")]
    public float LedgeGrabHeightOffset { get; private set; }
    [field: SerializeField, Tooltip("How 'forward' the the player's movement input needs to be to trigger a jump up onto the hung ledge.")]
    public float LedgeGrabJumpTolerance { get; private set; }
    [field: SerializeField, Tooltip("The force to use when jumping up onto the hung ledge.")]
    public float LedgeGrabJumpUpForce { get; private set; }
    [field: SerializeField, Tooltip("The time window in which pressing attack during a dodge will trigger a sword spin.")]
    public float SwordSpinWindow { get; private set; }
    [field: SerializeField] public float StandardAttackDuration { get; private set; }
    [field: SerializeField, Tooltip("The player must press attack at most this long after the previous attack state finished in order to chain the next attack.")]
    public float StandardAttackChainTime;
    [field: SerializeField] public float RollDuration { get; private set; }
    [field: SerializeField] public float RollSpeed { get; private set; }
    [field: SerializeField] public float RollDeceleration { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeSpeed { get; private set; }
    [field: SerializeField] public float DodgeDeceleration { get; private set; }
    [field: SerializeField] public float LandingDuration { get; private set; }
    [field: SerializeField] public float ClimbingReachTopDuration { get; private set; }
    [field: SerializeField] public float ClimbingStartDuration { get; private set; }
    [field: SerializeField] public float BowDrawDuration { get; private set; }
}
