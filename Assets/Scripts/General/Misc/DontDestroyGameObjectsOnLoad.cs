using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGameObjectsOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private void Awake()
    {
        foreach (GameObject obj in gameObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
