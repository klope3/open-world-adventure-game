using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMovement : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    public Vector3 direction;

    private void Update()
    {
        character.SetMovementDirection(direction);
    }

    public void RandomizeDirection()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        direction = new Vector3(rand.x, 0, rand.y);
    }

    private void OnDisable()
    {
        character.SetMovementDirection(Vector3.zero);
    }
}
