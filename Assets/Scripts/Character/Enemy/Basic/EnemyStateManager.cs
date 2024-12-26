using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class EnemyStateManager : StateManager<EnemyState>
{
    [SerializeField] private Character character;
    [SerializeField] private float wanderMoveSpeed;
    [SerializeField, Tooltip("The enemy will move for this long, then pause for this long, then repeat.")] 
    private float wanderMoveTime;
    [SerializeField] private float playerChaseDistance;
    [SerializeField] private float chaseSpeed;

    protected override string GetInitialStateName()
    {
        return "Wander";
    }

    protected override Dictionary<string, EnemyState> GetStateDictionary()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (!playerObj)
        {
            Debug.LogError("Could not find player!");
        }

        Dictionary<string, EnemyState> states = new Dictionary<string, EnemyState>();

        EnemyBasicWanderState wanderState = new EnemyBasicWanderState();
        EnemyBasicChaseState chaseState = new EnemyBasicChaseState();
        
        wanderState.Initialize(this, character, playerObj, transform, wanderMoveSpeed, wanderMoveTime, playerChaseDistance);
        chaseState.Initialize(this, character, playerObj, transform, playerChaseDistance, chaseSpeed);

        states.Add("Wander", wanderState);
        states.Add("Chase", chaseState);

        return states;
    }
}
