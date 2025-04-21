using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePersistentObjects : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectsParentPf;
    private bool initialized;
    public bool Initialized
    {
        get
        {
            return initialized;
        }
    }

    private void Awake()
    {
        GameObject existingInitializer = GameObject.FindGameObjectWithTag("InitializePersistentObjects");
        if (existingInitializer.GetComponent<InitializePersistentObjects>().Initialized) return;

        DontDestroyOnLoad(gameObject);
        GameObject obj = Instantiate(persistentObjectsParentPf);
        DontDestroyOnLoad(obj);

        initialized = true;
    }
}
