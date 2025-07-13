using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField, Tooltip("AudioSources assigned here will be randomly moved and played around the camera to simulate one-off ambient noises.")] 
    private AudioSource[] oneOffPlayers;
    
    [SerializeField, Tooltip("Minimum seconds between one-off ambient noises.")] 
    private float oneOffMinDelay;

    [SerializeField, Tooltip("Maximum seconds between one-off ambient noises.")]
    private float oneOffMaxDelay;

    [SerializeField, Tooltip("Minimum distance from the camera to play one-off ambient noises.")]
    private float oneOffMinDistance;

    [SerializeField, Tooltip("Maximum distance from the camera to play one-off ambient noises.")]
    private float oneOffMaxDistance;

    [SerializeField] private Camera mainCamera;
    private float oneOffTimer;
    private float oneOffTimerMax;

    private void Update()
    {
        oneOffTimer += Time.deltaTime;
        if (oneOffTimer > oneOffTimerMax)
        {
            PlayOneOff();
            oneOffTimer = 0;
            oneOffTimerMax = Random.Range(oneOffMinDelay, oneOffMaxDelay);
        }
    }

    private void PlayOneOff()
    {
        if (oneOffPlayers == null || oneOffPlayers.Length == 0) return;

        float randDistance = Random.Range(oneOffMinDistance, oneOffMaxDistance);
        Vector3 randOffset = Random.insideUnitSphere.normalized * randDistance;
        
        int randIndex = Random.Range(0, oneOffPlayers.Length);
        AudioSource randPlayer = oneOffPlayers[randIndex];

        randPlayer.transform.position = mainCamera.transform.position += randOffset;
        randPlayer.Play();
    }
}
