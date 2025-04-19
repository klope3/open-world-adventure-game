using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
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
        sceneTransition.TransitionToScene(sceneName);
    }
}
