using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the camera orbit the player in classic 3rd-person-camera style
public class CameraLookOrbiter : MonoBehaviour
{
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Vector3 angles;

    private void Update()
    {
        Vector2 inputVec = InputActionsProvider.GetSecondaryAxis();
        angles.x += inputVec.y * sensitivity * Time.deltaTime;
        angles.y += inputVec.x * sensitivity * Time.deltaTime;

        if (angles.x > 180) angles.x -= 360;
        if (angles.x < minX) angles.x = minX;
        if (angles.x > maxX) angles.x = maxX;

        cameraFollow.eulerAngles = angles;
    }

    public void SetCameraAngle(Vector3 angles)
    {
        this.angles = angles;
    }
}
