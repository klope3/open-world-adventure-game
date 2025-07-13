using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField, Tooltip("The player will use this spawn point in the target scene's PlayerInitialPositioner.")] 
    private int spawnPointIndex;
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        sceneTransition.TransitionToScene(sceneName, spawnPointIndex);
    }
}
