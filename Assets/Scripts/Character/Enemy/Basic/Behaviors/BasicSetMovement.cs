using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BasicSetMovement : MonoBehaviour
{
    [SerializeField] private EnemyStateManager stateManager;
    [SerializeField] private ECM2.Character character;
    [SerializeField] private MovementType movementType;
    [SerializeField, ShowIf("@this.movementType == MovementType.SingleVector")] private Vector3 vector;
    [SerializeField, ShowIf("@setSpeed")] private float speed;
    [SerializeField, Tooltip("If true, updates the movement direction according to the movement type each frame. If false, sets the direction once in OnEnable.")] 
    private bool updateDirection;
    [SerializeField] private bool setSpeed;
    [SerializeField, ShowIf("@preferHeight")] private float preferredHeight;
    [SerializeField, ShowIf("@preferHeight")] private RaycastChecker raycastChecker;
    [SerializeField, Tooltip("If true, will continuously add an up or down vector to try to maintain the given preferredHeight. Needs a reference to a RaycastChecker to detect current height.")] 
    private bool preferHeight;
    private Vector3 initialVec;

    public enum MovementType
    {
        SingleVector,
        RandomFlatVector,
        TowardPlayer,
    }

    private void OnEnable()
    {
        initialVec = ChooseMovementDirection();
        character.SetMovementDirection(initialVec);
        if (setSpeed) character.maxWalkSpeed = speed;
    }

    private void Update()
    {
        if (!updateDirection)
        {
            if (preferHeight) character.SetMovementDirection(initialVec + CalculateHeightAdjustmentVector());
            return;
        }
        character.SetMovementDirection(ChooseMovementDirection() + CalculateHeightAdjustmentVector());
    }

    private Vector3 CalculateHeightAdjustmentVector()
    {
        if (!preferHeight) return Vector3.zero;

        bool hit = raycastChecker.CheckWithInfo(out RaycastHit hitInfo);
        if (!hit) return Vector3.zero;

        float currentHeight = Vector3.Distance(stateManager.transform.position, hitInfo.point);
        if (currentHeight > preferredHeight) return Vector3.down;
        return Vector3.up;
    }

    private Vector3 ChooseMovementDirection()
    {
        if (movementType == MovementType.TowardPlayer) return GetFlattenedVectorToPlayer();
        if (movementType == MovementType.RandomFlatVector) return PickRandomDirection();
        return vector;
    }

    private Vector3 GetFlattenedVectorToPlayer()
    {
        Vector3 vecToPlayer = stateManager.PlayerObject.transform.position - stateManager.transform.position;
        Vector3 flattened = new Vector3(vecToPlayer.x, 0, vecToPlayer.z);
        return flattened;
    }

    private Vector3 PickRandomDirection()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        return new Vector3(rand.x, 0, rand.y);
    }
}
