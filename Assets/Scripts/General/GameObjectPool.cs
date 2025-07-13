using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int startingCount;
    [SerializeField] private bool instantiateOnAwake;
    private List<GameObject> pooledObjects;
    public delegate void GameObjectEvent(GameObject gameObject);
    public GameObjectEvent OnObjectInstantiated;

    private void Awake()
    {
        if (instantiateOnAwake) Initialize();
    }

    public void Initialize()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < startingCount; i++)
        {
            CreatePooledObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            GameObject obj = pooledObjects[i];
            if (!obj.activeSelf)
            {
                return obj;
            } 
        }

        return CreatePooledObject();
    }

    private GameObject CreatePooledObject()
    {
        GameObject go = InstantiateObject(prefabToPool);
        go.transform.SetParent(transform);
        go.SetActive(false);
        pooledObjects.Add(go);
        OnObjectInstantiated?.Invoke(go);
        return go;
    }

    public void SetPrefab(GameObject prefab)
    {
        prefabToPool = prefab;
    }

    //this can be overridden by inheriting classes that need to initialize objects with references
    //or do other logic before/after instantiation
    protected virtual GameObject InstantiateObject(GameObject prefab)
    {
        return Instantiate(prefab);
    }
}
