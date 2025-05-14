using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicAttackState : EnemyState
{
    //private HealthHandler healthHandler;
    //private float attackSpeed;
    //private float attackDuration;
    //private float timer;
    private bool hurt;

    //public System.Action OnEnter;
    //public System.Action OnExit;

    public override void EnterState()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.OwnTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);

        stateManager.Character.SetMovementDirection(flattened);
        stateManager.Character.maxWalkSpeed = stateManager.AttackSpeed;

        stateManager.OwnHealth.OnDamaged += HealthHandler_OnDamaged;

        //OnEnter?.Invoke();
    }

    private void HealthHandler_OnDamaged(Vector3 position)
    {
        //stateManager.SwitchState("Pause");
        //timer = 0;
        hurt = true;
    }

    public override void ExitState()
    {
        stateManager.OwnHealth.OnDamaged -= HealthHandler_OnDamaged;
        hurt = false;
        //OnExit?.Invoke();
    }

    public override void UpdateState()
    {
        //if (stateManager.PlayerObject.GetComponent<HealthHandler>().CurHealth == 0)
        //{
        //    stateManager.SwitchState("Wander");
        //    return;
        //}
        //
        //timer += Time.deltaTime;
        //if (timer > stateManager.AttackDuration)
        //{
        //    stateManager.SwitchState("Pause");
        //    timer = 0;
        //}
    }

    //public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, HealthHandler ownHealth, float attackSpeed, float attackDuration)
    //{
    //    Initialize(stateManager, character, playerObj, ownTransform);
    //    this.attackSpeed = attackSpeed;
    //    this.attackDuration = attackDuration;
    //    this.healthHandler = ownHealth;
    //}

    public override string GetDebugName()
    {
        return "attack";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(EnemyStateManager.PAUSE_STATE, ToPause),
            new StateTransition(EnemyStateManager.WANDER_STATE, () => stateManager.PlayerObject.GetComponent<HealthHandler>().CurHealth <= 0)
        };
    }

    private bool ToPause()
    {
        return hurt || stateManager.TimeInState > stateManager.AttackDuration;
    }
}
