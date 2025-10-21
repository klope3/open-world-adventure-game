using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlatformPuzzle : MonoBehaviour
{
    [SerializeField] private Transform platformOne;
    [SerializeField] private Transform platformTwo;
    [SerializeField] private Transform platformThree;
    [SerializeField] private Transform loweredHeight;
    [SerializeField] private Transform raisedHeight;
    [SerializeField] private float platformMoveDuration;
    [SerializeField, Tooltip("How long after an input before another input will be accepted.")] private float inputDelay;
    private bool allowInput = true;
    public UnityEvent OnPlatformMoveStart;
    public UnityEvent OnPlatformMoveEnd;
    public UnityEvent OnPlatformOneMoveStart;
    public UnityEvent OnPlatformTwoMoveStart;
    public UnityEvent OnPlatformThreeMoveStart;

    public void ReceiveInput(int switchNumber)
    {
        if (!allowInput || switchNumber < 1 || switchNumber > 3) return;

        if (switchNumber == 1)
        {
            TogglePlatform(1);
            TogglePlatform(2);
        }
        if (switchNumber == 2)
        {
            TogglePlatform(1);
            TogglePlatform(3);
        }
        if (switchNumber == 3)
        {
            TogglePlatform(3);
        }

        StartCoroutine(CO_Transition());
    }

    private void TogglePlatform(int number)
    {
        Transform platform = GetTransformByNumber(number);
        bool lowered = platform.position.y < raisedHeight.position.y;
        float newY = lowered ? raisedHeight.position.y : loweredHeight.position.y;
        platform.DOMove(new Vector3(platform.position.x, newY, platform.position.z), platformMoveDuration);

        if (number == 1) OnPlatformOneMoveStart?.Invoke();
        if (number == 2) OnPlatformTwoMoveStart?.Invoke();
        if (number == 3) OnPlatformThreeMoveStart?.Invoke();
        OnPlatformMoveStart?.Invoke();
    }

    private Transform GetTransformByNumber(int number)
    {
        if (number == 1) return platformOne;
        if (number == 2) return platformTwo;
        if (number == 3) return platformThree;
        return null;
    }

    //transition between states after an input is successfully received
    private IEnumerator CO_Transition()
    {
        allowInput = false;
        yield return new WaitForSeconds(inputDelay);
        allowInput = true;
        OnPlatformMoveEnd?.Invoke();
    }
}
