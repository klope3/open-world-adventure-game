using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChestAnimationEvents : MonoBehaviour
{
    public System.Action ChestSmall_OnHandsRaised;
    public System.Action ChestSmall_OnKicked;
    public System.Action ChestSmall_OnCameraZoomStart;

    public void ChestSmall_CameraZoomStart()
    {
        ChestSmall_OnCameraZoomStart?.Invoke();
    }

    public void ChestSmall_HandsRaised()
    {
        ChestSmall_OnHandsRaised?.Invoke();
    }

    public void ChestSmall_OnKick()
    {
        ChestSmall_OnKicked?.Invoke();
    }
}
