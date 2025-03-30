using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicAttackState : EnemyState
{
    private HealthHandler healthHandler;
    private float attackSpeed;
    private float attackDuration;
    private float timer;

    public System.Action OnEnter;
    public System.Action OnExit;

    public override void EnterState()
    {
        Vector3 vecToPlayer = playerObj.transform.position - ownTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);

        character.SetMovementDirection(flattened);
        character.maxWalkSpeed = attackSpeed;

        healthHandler.OnDamaged += HealthHandler_OnDamaged;

        OnEnter?.Invoke();
    }

    private void HealthHandler_OnDamaged(Vector3 position)
    {
        stateManager.SwitchState("Pause");
        timer = 0;
    }

    public override void ExitState()
    {
        healthHandler.OnDamaged -= HealthHandler_OnDamaged;
        OnExit?.Invoke();
    }

    public override void UpdateState()
    {
        if (playerObj.GetComponent<HealthHandler>().CurHealth == 0)
        {
            stateManager.SwitchState("Wander");
            return;
        }

        timer += Time.deltaTime;
        if (timer > attackDuration)
        {
            stateManager.SwitchState("Pause");
            timer = 0;
        }
    }

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, HealthHandler ownHealth, float attackSpeed, float attackDuration)
    {
        Initialize(stateManager, character, playerObj, ownTransform);
        this.attackSpeed = attackSpeed;
        this.attackDuration = attackDuration;
        this.healthHandler = ownHealth;
    }

    public override string GetDebugName()
    {
        return "attack";
    }
}
