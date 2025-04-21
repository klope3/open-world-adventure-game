using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
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

        SceneTransition sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransition>();
        sceneTransition.TransitionToScene(sceneName, spawnPointIndex);
    }
}
