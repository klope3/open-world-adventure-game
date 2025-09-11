using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputActionsProvider
{
    private static InputActions inputActions;
    private static InputActions InputActions
    {
        get
        {
            if (inputActions == null)
            {
                inputActions = new InputActions();
                inputActions.Player.Enable();

                inputActions.Player.InteractButton.started += InteractButton_started;
                inputActions.Player.AButton.started += AButton_started;
                inputActions.Player.AButton.canceled += AButton_canceled;
                inputActions.Player.BButton.started += BButton_started;
                inputActions.Player.BButton.canceled += BButton_canceled;
                inputActions.Player.ZTarget.started += ZTarget_started;
                inputActions.Player.DodgeButton.started += DodgeButton_started;
                inputActions.Player.SwapButton.started += SwapButton_started;
                inputActions.Player.PauseButton.started += PauseButton_started;
                inputActions.Player.BlockButton.started += BlockButton_started;
                inputActions.Player.BlockButton.canceled += BlockButton_canceled;
            }
            return inputActions;
        }
    }

    public static System.Action OnInteractButtonStarted;
    public static System.Action OnAButtonStarted;
    public static System.Action OnAButtonCanceled;
    public static System.Action OnBButtonStarted;
    public static System.Action OnBButtonCanceled;
    public static System.Action OnZTargetStarted;
    public static System.Action OnDodgeButtonStarted;
    public static System.Action OnSwapButtonStarted;
    public static System.Action OnPauseButtonStarted;
    public static System.Action OnBlockButtonStarted;
    public static System.Action OnBlockButtonCanceled;

    private static bool overridePrimaryAxis;
    private static Vector3 primaryAxisOverrideVec;

    public static void LockPrimaryAxisTo(Vector3 lockedVec)
    {
        primaryAxisOverrideVec = lockedVec;
        overridePrimaryAxis = true;
    }

    public static void UnlockPrimaryAxis()
    {
        overridePrimaryAxis = false;
    }

    private static void BlockButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBlockButtonStarted?.Invoke();
    }

    private static void BlockButton_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBlockButtonCanceled?.Invoke();
    }

    private static void PauseButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseButtonStarted?.Invoke();
    }

    private static void SwapButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwapButtonStarted?.Invoke();
    }

    private static void DodgeButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDodgeButtonStarted?.Invoke();
    }

    private static void ZTarget_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnZTargetStarted?.Invoke();
    }

    private static void BButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBButtonStarted?.Invoke();
    }

    private static void BButton_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnBButtonCanceled?.Invoke();
    }

    private static void InteractButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractButtonStarted?.Invoke();
    }

    private static void AButton_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAButtonStarted?.Invoke();
    }

    private static void AButton_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAButtonCanceled?.Invoke();
    }

    public static Vector2 GetSnappedPrimaryAxis()
    {
        Vector2 inputVec = GetPrimaryAxis();
        Vector3 snappedVec = Utils.SnapToOrthogonalVector(new Vector3(inputVec.x, 0, inputVec.y));
        return new Vector2(snappedVec.x, snappedVec.z);
    }

    public static Vector2 GetPrimaryAxis()
    {
        return overridePrimaryAxis ? primaryAxisOverrideVec : InputActions.Player.PrimaryDirectionalAxis.ReadValue<Vector2>();
    }

    public static Vector2 GetSecondaryAxis()
    {
        return InputActions.Player.SecondaryDirectionalAxis.ReadValue<Vector2>();
    }
}
