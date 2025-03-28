using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float minRandomPitch;
    [SerializeField] private float maxRandomPitch;
    [SerializeField, Tooltip("If greater than zero, helps prevent the sound player from playing again too soon after the last play.")] 
    private float minTimeBetweenPlays;
    private float timeSinceLastPlay;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timeSinceLastPlay += Time.deltaTime;
    }

    public void PlayRandom()
    {
        if (clips.Length == 0) return;
        if (minTimeBetweenPlays > 0 && timeSinceLastPlay < minTimeBetweenPlays) return;

        int randIndex = Random.Range(0, clips.Length);
        source.clip = clips[randIndex];
        source.pitch = Random.Range(minRandomPitch, maxRandomPitch);
        source.Play();
        timeSinceLastPlay = 0;
    }
}
