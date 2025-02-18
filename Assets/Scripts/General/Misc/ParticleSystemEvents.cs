using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Unity currently has very limited support for events in particle systems.
//This helps get around that limitation in many cases.
[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemEvents : MonoBehaviour
{
    private ParticleSystem ps;
    private float particleCountLastFrame;
    public UnityEvent OnParticleEmitted;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps.particleCount > particleCountLastFrame) OnParticleEmitted?.Invoke(); //note that if more than one particle was emitted during a frame, the event will still only be called once.
        particleCountLastFrame = ps.particleCount;
    }
}
