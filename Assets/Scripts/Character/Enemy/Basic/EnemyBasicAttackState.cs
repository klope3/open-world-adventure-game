using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyBasicAttackState : EnemyState
{
    private float attackSpeed;
    private float attackDuration;
    private float timer;

    public System.Action OnEnter;

    public override void EnterState()
    {
        Vector3 vecToPlayer = playerObj.transform.position - ownTransform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);

        character.SetMovementDirection(flattened);
        character.maxWalkSpeed = attackSpeed;

        OnEnter?.Invoke();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer > attackDuration)
        {
            stateManager.SwitchState("Pause");
            timer = 0;
        }
    }

    public void Initialize(EnemyStateManager stateManager, Character character, GameObject playerObj, Transform ownTransform, float attackSpeed, float attackDuration)
    {
        Initialize(stateManager, character, playerObj, ownTransform);
        this.attackSpeed = attackSpeed;
        this.attackDuration = attackDuration;
    }
}
