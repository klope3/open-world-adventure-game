using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePersistentObjects : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectsParentPf;
    [SerializeField] private GameObjectPool arrowPool;
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

        PlayerInitializer playerInitializer = obj.GetComponentInChildren<PlayerInitializer>();
        playerInitializer.Initialize(arrowPool);

        initialized = true;
    }
}
