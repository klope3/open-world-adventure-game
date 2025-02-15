using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEvents : MonoBehaviour
{
    public UnityEvent OnGenericAttack;
    public UnityEvent OnSwordSlashStart;
    public UnityEvent OnJumpStart;
    public UnityEvent OnRollStart;
    public UnityEvent OnDodgeStart;
    public UnityEvent OnFootstepNormal;

    public void Attack_Generic()
    {
        OnGenericAttack?.Invoke();
    }

    public void Attack_SwordSlashStart()
    {
        OnSwordSlashStart?.Invoke();
    }

    public void Movement_JumpStart()
    {
        OnJumpStart?.Invoke();
    }

    public void Movement_RollStart()
    {
        OnRollStart?.Invoke();
    }

    public void Movement_DodgeStart()
    {
        OnDodgeStart?.Invoke();
    }

    public void Movement_FootstepNormal()
    {
        OnFootstepNormal?.Invoke();
    }
}
