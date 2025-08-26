using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class CharacterCollisionDamage : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private int amount;

    private void OnEnable()
    {
        character.Collided += Character_Collided;
    }

    private void Character_Collided(ref CollisionResult collisionResult)
    {
        HealthHandler healthHandler = collisionResult.collider.GetComponent<HealthHandler>();
        if (healthHandler == null) return;
        healthHandler.AddHealth(-1 * amount, collisionResult.point);
    }

    private void OnDisable()
    {
        character.Collided -= Character_Collided;
    }
}
