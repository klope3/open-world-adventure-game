using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class AttackState : PlayerState
{
    private Collider damageZone;
    private float timeInState;
    private readonly float damageZoneTimerMax = 0.1f;
    private float damageZoneTimer;

    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        Debug.Log("Entering attack");
        damageZoneTimer = 0;
        damageZone.gameObject.SetActive(true);
        characterAdapter.canMove = false;

        OnEnter?.Invoke();
    }
    
    public override void UpdateState()
    {
        if (damageZoneTimer < damageZoneTimerMax)
        {
            damageZoneTimer += Time.deltaTime;
        } else
        {
            damageZone.gameObject.SetActive(false);
        }

        if (stateManager.TimeInState > timeInState)
        {
            stateManager.SwitchState("Idle");
        }
    }
    
    public override void ExitState()
    {
        Debug.Log("Exiting attack");
        OnExit?.Invoke();
    }

    public void Initialize(PlayerStateManager stateManager, Character character, ECM2CharacterAdapter characterAdapter, Collider damageZone, float timeInState)
    {
        Initialize(stateManager, character, characterAdapter);
        this.damageZone = damageZone;
        this.timeInState = timeInState;
    }

    public override void PostInitialize()
    {

    }
}
