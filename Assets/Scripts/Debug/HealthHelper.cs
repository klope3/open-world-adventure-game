using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//helps with fake-initializing health while prototyping and no save game is loaded to determine starting health
public class HealthHelper : MonoBehaviour
{
    [SerializeField] private HealthHandler playerHealth;

    private void Awake()
    {
        StartCoroutine(CO_Run());
    }

    private IEnumerator CO_Run()
    {
        yield return new WaitForSeconds(3);
        playerHealth.Initialize(12, 12);
    }
}
