using Animancer;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimancerTest : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip bowUp;
    [SerializeField] private AnimationClip bowDown;
    [SerializeField] private AnimationClip testClip;
    [SerializeField] private AvatarMask bowLookMask;
    [SerializeField] private LinearMixerTransition testTransition;
    [SerializeField] private TransitionAssetBase strafeTransitionAsset;
    [SerializeField] private StringAsset strafeParameterX;
    [SerializeField] private StringAsset strafeParameterY;
    public float testParam;
    public Vector2 testVec;

    //private LinearMixerState bowMixer;
    private SmoothedVector2Parameter smoothedParameters;

    private void Awake()
    {
        smoothedParameters = new SmoothedVector2Parameter(
            animancer,
            strafeParameterX,
            strafeParameterY,
            0);

        animancer.Play(strafeTransitionAsset);

        //bowMixer = new LinearMixerState
        //{
        //    { bowDown, 0 },
        //    { bowUp, 1 }
        //};
        animancer.Layers[1].Mask = bowLookMask;
        //animancer.Layers[1].IsAdditive = true;
        //animancer.Layers[1].Play(bowMixer);
    }

    private void Update()
    {
        if (testTransition.State != null) testTransition.State.Parameter = testParam;

        //bowMixer.Parameter = testParam;
        smoothedParameters.TargetValue = testVec;
    }

    [Button]
    public void StartBow()
    {
        animancer.Layers[1].Play(testTransition);
    }

    [Button]
    public void StopBow()
    {
        animancer.Layers[1].Weight = 0;
    }
}