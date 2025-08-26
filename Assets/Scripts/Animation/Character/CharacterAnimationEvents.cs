using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationEvents : MonoBehaviour
{
    public UnityEvent OnGenericAttackStart;
    public UnityEvent OnGenericAttackEnd;
    public UnityEvent OnJumpStart;
    public UnityEvent OnFootstepNormal;

    public void Attack_Generic_Start()
    {
        OnGenericAttackStart?.Invoke();
    }

    public void Attack_Generic_End()
    {
        OnGenericAttackEnd?.Invoke();
    }

    public void Movement_FootstepNormal()
    {
        OnFootstepNormal?.Invoke();
    }

    public void Movement_JumpStart()
    {
        OnJumpStart?.Invoke();
    }
}
