using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private float timeScale;

    private void Awake()
    {
        Time.timeScale = timeScale;
    }
}
