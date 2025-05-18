using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingModule : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField, Tooltip("Climbing happens in movement 'increments,' which cover this much distance")]
    private float movementIncrementDistance;
    [SerializeField, Tooltip("Short delay after reaching the destination position to ensure player doesn't start moving again too soon")] 
    private float movementIncrementFinishDelay;
    [SerializeField] private float climbingSpeed;
    public System.Action OnClimbUpLeft; //the character's left hand moves up
    public System.Action OnClimbUpRight; //the character's right hand moves up
    public System.Action OnClimbingDownStart;
    public System.Action OnClimbingDownStop;
    public System.Action OnLeftHandMoveUp;
    public System.Action OnRightHandMoveUp;
    public System.Action OnLeftHandMoveDown;
    public System.Action OnRightHandMoveDown;
    private float curYDirection;
    private Vector3 movementIncrementStartPos; //transform.position when the current movement increment started
    private bool moving;
    private bool isFinishDelayRunning;
    private float prevMoveSpeed;
    private Hand currentHandUp;
    private enum Hand
    {
        Left,
        Right,
    }

    private void OnEnable()
    {
        currentHandUp = Hand.Right;
        prevMoveSpeed = character.maxFlySpeed;
        character.maxFlySpeed = climbingSpeed;
    }

    private void OnDisable()
    {
        character.maxFlySpeed = prevMoveSpeed;
        moving = false;
    }

    private void Update()
    {
        if (isFinishDelayRunning) return;

        if (moving)
        {
            float distanceTraveled = Vector3.Distance(character.transform.position, movementIncrementStartPos);
            if (distanceTraveled >= movementIncrementDistance)
            {
                Vector3 destinationPos = movementIncrementStartPos + Vector3.up * curYDirection * movementIncrementDistance;
                character.TeleportPosition(destinationPos); //snap to dest position to help prevent floating point drift during long climbing sessions
                character.SetVelocity(Vector3.zero);
                character.SetMovementDirection(Vector3.zero);
                StartCoroutine(CO_MovementFinishDelay());
            }
        } else
        {
            Vector3 primaryAxis = InputActionsProvider.GetPrimaryAxis();
            curYDirection = primaryAxis.y > 0.05f ? 1 : primaryAxis.y < -0.05f ? -1 : 0;
            if (curYDirection != 0)
            {
                movementIncrementStartPos = character.transform.position;
                character.SetMovementDirection(new Vector3(0, curYDirection, 0));
                moving = true;
                HandleHandMovement();
            }
        }
    }

    private IEnumerator CO_MovementFinishDelay()
    {
        isFinishDelayRunning = true;
        yield return new WaitForSeconds(movementIncrementFinishDelay);
        moving = false;
        isFinishDelayRunning = false;
    }

    private void HandleHandMovement()
    {
        if (currentHandUp == Hand.Left)
        {
            currentHandUp = Hand.Right;
            OnRightHandMoveUp?.Invoke();
        } else
        {
            currentHandUp = Hand.Left;
            OnLeftHandMoveUp?.Invoke();
        }
    }
}
