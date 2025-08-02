using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//NOTE: In order to detect that a GameObject is no longer in the DetectorZone when that object is disabled/destroyed, that object will need a GameObjectEvents script attached.
[RequireComponent(typeof(Collider))]
public class GameObjectDetectorZone : MonoBehaviour
{
    private HashSet<GameObject> objectsInside;
    public delegate void GameObjectEvent(GameObject obj);
    public event GameObjectEvent OnObjectEntered;
    public event GameObjectEvent OnObjectExited;
    [ShowInInspector, DisplayAsString] public int ObjectCount
    {
        get
        {
            if (objectsInside == null) return 0;
            return objectsInside.Count;
        }
    }

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        objectsInside = new HashSet<GameObject>();
    }

    private void Update()
    {
        //this might be expensive to do in Update and could be optimized later if needed
        List<GameObject> invalidObjects = new List<GameObject>();
        foreach (GameObject obj in objectsInside)
        {
            if (!IsObjectValid(obj)) invalidObjects.Add(obj);
        }
        foreach (GameObject obj in invalidObjects) 
        {
            ObjectExit(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsObjectValid(other.gameObject)) return;

        objectsInside.Add(other.gameObject);
        OnObjectEntered?.Invoke(other.gameObject);
        GameObjectEvents events = other.GetComponent<GameObjectEvents>();
        if (events)
        {
            events.OnDisabled += ObjectExit;
            events.OnDestroyed += ObjectExit;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!objectsInside.Contains(other.gameObject)) return;
        ObjectExit(other.gameObject);
    }

    private void ObjectExit(GameObject obj)
    {
        objectsInside.Remove(obj);
        OnObjectExited?.Invoke(obj);
        GameObjectEvents events = obj == null ? null : obj.GetComponent<GameObjectEvents>();
        if (events)
        {
            events.OnDisabled -= ObjectExit;
            events.OnDestroyed -= ObjectExit;
        }
    }

    public List<GameObject> GetObjectsList()
    {
        return new List<GameObject>(objectsInside);
    }

    /// <summary>
    /// Override this method to allow different inheriters of GameObjectDetectorZone to filter out certain kinds of objects. If an object should be detectable, return true.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected virtual bool IsObjectValid(GameObject obj)
    {
        return true;
    }
}
