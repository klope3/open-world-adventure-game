using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    public UnityEvent OnSwordSlashStart;
    public UnityEvent OnSwordSlashEnd;
    public UnityEvent OnRollStart;
    public UnityEvent OnDodgeStart;

    public void Attack_SwordSlashStart()
    {
        OnSwordSlashStart?.Invoke();
    }

    public void Attack_SwordSlashEnd()
    {
        OnSwordSlashEnd?.Invoke();
    }

    public void Movement_RollStart()
    {
        OnRollStart?.Invoke();
    }

    public void Movement_DodgeStart()
    {
        OnDodgeStart?.Invoke();
    }
}
