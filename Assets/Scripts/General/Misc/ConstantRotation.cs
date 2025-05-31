using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private Vector3 angleAdd;

    private void Update()
    {
        Vector3 newAngles = transform.eulerAngles;
        newAngles += angleAdd * Time.deltaTime;
        transform.eulerAngles = newAngles;
    }
}
