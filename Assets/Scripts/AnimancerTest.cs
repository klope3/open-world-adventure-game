using Animancer;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimancerTest : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private LinearMixerTransition bowMixer;
    public float testParam;

    private void Awake()
    {
        animancer.Layers[1].Play(bowMixer);
    }

    private void Update()
    {
        if (bowMixer.State != null) bowMixer.State.Parameter = testParam;
    }
}