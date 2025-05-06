using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimancerTestInput : MonoBehaviour
{
    [SerializeField] private Transform objTrans;
    [SerializeField] private float fac;

    public Vector2 GetInput()
    {
        return Vector2.ClampMagnitude(new Vector2(objTrans.position.x, objTrans.position.z) * fac, 1);
    }
}
