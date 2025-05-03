using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingModule : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private float handTriggerTime;
    public System.Action OnClimbUpLeft; //the character's left hand moves up
    public System.Action OnClimbUpRight; //the character's right hand moves up
    public System.Action OnClimbingDownStart;
    public System.Action OnClimbingDownStop;
    public System.Action OnLeftHandMoveUp;
    public System.Action OnRightHandMoveUp;
    public System.Action OnLeftHandMoveDown;
    public System.Action OnRightHandMoveDown;
    private bool readyForNextHand;
    private Hand currentHandUp;
    private enum Hand
    {
        Left,
        Right,
    }

    private void OnEnable()
    {
        currentHandUp = Hand.Left;
        readyForNextHand = true;
    }

    private void Update()
    {
        Vector3 primaryAxis = InputActionsProvider.GetPrimaryAxis();

        Vector3 moveVec = new Vector3();

        moveVec.y = primaryAxis.y > 0.05f ? 1 : primaryAxis.y < -0.05f ? -1 : 0;
        character.SetMovementDirection(moveVec);

        HandleAnimation(moveVec.y);
    }

    private IEnumerator CO_HandTimer()
    {
        readyForNextHand = false;
        yield return new WaitForSeconds(handTriggerTime);
        readyForNextHand = true;
    }

    private void HandleAnimation(float moveY)
    {
        if (!readyForNextHand) return;

        if (moveY > 0.05f)
        {
            if (currentHandUp == Hand.Left)
            {
                currentHandUp = Hand.Right;
                OnRightHandMoveUp?.Invoke();
            }
            else
            {
                currentHandUp = Hand.Left;
                OnLeftHandMoveUp?.Invoke();
            }
            StartCoroutine(CO_HandTimer());
        }

        if (moveY < -0.05f)
        {
            if (currentHandUp == Hand.Left)
            {
                currentHandUp = Hand.Right;
                OnLeftHandMoveDown?.Invoke();
            }
            else
            {
                currentHandUp = Hand.Left;
                OnRightHandMoveDown?.Invoke();
            }
            StartCoroutine(CO_HandTimer());
        }
    }
}
