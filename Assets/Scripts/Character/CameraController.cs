using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private DirectionalInputProvider inputProvider;
    [SerializeField] private Transform cameraFollow;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Vector3 angles; 

    private void Update()
    {
        Vector3 initialAngles = new Vector3(angles.x, angles.y, angles.z);
        Vector2 inputVec = inputProvider.GetInput();
        angles.x += inputVec.y * sensitivity * Time.deltaTime;
        angles.y += inputVec.x * sensitivity * Time.deltaTime;

        if (angles.x < minX) angles.x = minX;
        if (angles.x > maxX) angles.x = maxX;

        cameraFollow.eulerAngles = angles;
    }
}
