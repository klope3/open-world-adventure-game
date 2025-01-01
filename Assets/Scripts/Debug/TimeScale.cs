using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private float timeScale;

    private void Awake()
    {
        Time.timeScale = timeScale;
    }

    [Button]
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
