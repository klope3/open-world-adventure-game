using Animancer;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimancerTest : MonoBehaviour
{
    [SerializeField] private AnimancerTestInput input;
    [SerializeField] private AnimancerComponent _Animancer;
    [SerializeField] private TransitionAssetBase strafeTransitionAsset;
    [SerializeField] private StringAsset strafeParameterX;
    [SerializeField] private StringAsset strafeParameterY;
    [SerializeField] private AnimationClip run;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip walk;
    [SerializeField] private AnimationClip slash;
    [SerializeField] private float fade;

    private LinearMixerState mixer;
    private SmoothedVector2Parameter smoothedParameters;

    private void Awake()
    {
        smoothedParameters = new SmoothedVector2Parameter(
            _Animancer,
            strafeParameterX,
            strafeParameterY,
            0);
        _Animancer.Play(strafeTransitionAsset);
        //mixer = new LinearMixerState();
        //mixer.Add(idle, 0);
        //mixer.Add(walk, 0.5f);
        //mixer.Add(run, 1);
        //_Animancer.Play(mixer);
    }

    private void Update()
    {
        Vector2 moveInput = input.GetInput();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(moveInput);
        smoothedParameters.TargetValue = new Vector2(squareVec.x, squareVec.y);

        //mixer.Parameter = moveInput.y;
    }

    [Button]
    public void Run()
    {
        _Animancer.Play(run, fade);

        // You can manipulate the animation using the returned AnimancerState:
        //AnimancerState state = _Animancer.Play(_Clip);
        //state.Speed = ...                  // See the Fine Control samples.
        //state.Time = ...                   // See the Fine Control samples.
        //state.NormalizedTime = ...         // See the Fine Control samples.
        //state.Events(this).OnEnd = ...     // See End Events.

        // If the animation was already playing, it will continue from the current time.
        // So to force it to play from the beginning you can just reset the Time:
        //_Animancer.Play(_Clip).Time = 0;
    }

    [Button]
    public void Idle()
    {
        _Animancer.Play(idle, fade);
    }

    [Button]
    public void Slash()
    {
        _Animancer.Play(slash, fade);
    }
}