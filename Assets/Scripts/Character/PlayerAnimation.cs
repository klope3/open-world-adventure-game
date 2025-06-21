using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Animancer;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Rigidbody characterRb;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PlayerDefaultMovementModule defaultMovementModule;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthHandler health;
    [SerializeField] private PlayerClimbingModule climbingModule;

    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private TransitionAssetBase strafeTransitionAsset;
    [SerializeField] private StringAsset strafeParameterX;
    [SerializeField] private StringAsset strafeParameterY;

    [SerializeField] private TransitionAssetBase roll;
    [SerializeField] private TransitionAssetBase attack1;
    [SerializeField] private TransitionAssetBase attack2;
    [SerializeField] private AnimationClip falling;
    [SerializeField] private TransitionAssetBase land;
    [SerializeField] private TransitionAssetBase jump;

    [SerializeField] private AnimationClip ladderIdle;
    [SerializeField] private AnimationClip ladderClimbUpLeftHand;
    [SerializeField] private AnimationClip ladderClimbUpRightHand;
    [SerializeField] private AnimationClip ladderClimbDownLeftHand;
    [SerializeField] private AnimationClip ladderClimbDownRightHand;
    [SerializeField] private AnimationClip ladderReachTop;
    [SerializeField] private AnimationClip ladderStart;

    [SerializeField] private AnimationClip dodgeForward;
    [SerializeField] private AnimationClip dodgeLeft;
    [SerializeField] private AnimationClip dodgeBack;
    [SerializeField] private AnimationClip dodgeRight;

    [SerializeField] private AnimationClip chestSmallOpen;

    private RedirectRootMotionToTransform rootMotionRedirector;
    private SmoothedVector2Parameter smoothedParameters;
    private RigidbodyInterpolation prevRbInterpolation; //stored when we turn off interpolation at the start of a root motion animation, so we can put the RB back to the way it was afterward

    private readonly float DEFAULT_FADE_DURATION = 0.1f;

    private void Awake()
    {
        smoothedParameters = new SmoothedVector2Parameter(
            animancer,
            strafeParameterX,
            strafeParameterY,
            0);

        playerStateManager.OnDefaultState += PlayerStateManager_OnDefaultState;
        playerStateManager.OnAttack2 += PlayerStateManager_OnAttack2;
        playerStateManager.OnLeftGround += PlayerStateManager_OnLeftGround;
        playerStateManager.OnLand += PlayerStateManager_OnLand;
        playerStateManager.OnStateChange += PlayerStateManager_OnStateChange;

        climbingModule.OnLeftHandMoveUp += Climbing_LeftHandMoveUp;
        climbingModule.OnRightHandMoveUp += Climbing_RightHandMoveUp;
    }

    private void Climbing_LeftHandMoveUp()
    {
        animancer.Play(ladderClimbUpLeftHand);
    }

    private void Climbing_RightHandMoveUp()
    {
        animancer.Play(ladderClimbUpRightHand);
    }

    private void PlayerStateManager_OnStateChange(string stateName)
    {
        //this should probably be a dictionary or something
        if (stateName == PlayerStateManager.DEFAULT_STATE) animancer.Play(strafeTransitionAsset);
        if (stateName == PlayerStateManager.ATTACK_STATE) animancer.Play(attack1);
        if (stateName == PlayerStateManager.ATTACK2_STATE) animancer.Play(attack2);
        if (stateName == PlayerStateManager.JUMPING_STATE) animancer.Play(jump);
        if (stateName == PlayerStateManager.FALLING_STATE) animancer.Play(falling, DEFAULT_FADE_DURATION);
        if (stateName == PlayerStateManager.ROLL_STATE) animancer.Play(roll);
        if (stateName == PlayerStateManager.LANDING_STATE) animancer.Play(land);
        if (stateName == PlayerStateManager.CLIMBING_STATE) animancer.Play(ladderIdle, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
        if (stateName == PlayerStateManager.CLIMBING_REACH_TOP_STATE) PlayRootMotionAnimation(ladderReachTop);
        if (stateName == PlayerStateManager.CLIMBING_START_STATE) PlayRootMotionAnimation(ladderStart);
        if (stateName == PlayerStateManager.DODGING_STATE) PlayDodgeAnimation();
        if (stateName == PlayerStateManager.LOOT_STATE) PlayRootMotionAnimation(chestSmallOpen);
    }

    private void Update()
    {
        DefaultMovementAnimation();
    }
    
    private void DefaultMovementAnimation()
    {
        if (playerStateManager.CurrentStateKey != PlayerStateManager.DEFAULT_STATE) return;

        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(inputVec);

        float xComponent = defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly ? 0 : squareVec.x;
        float yComponent = defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly ? squareVec.magnitude : squareVec.y;
        smoothedParameters.TargetValue = new Vector2(xComponent, yComponent);
    }

    private void PlayDodgeAnimation()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        float arctan = Mathf.Atan2(inputVec.y, inputVec.x);
        float degrees = arctan * Mathf.Rad2Deg;
        if (degrees < 0) degrees += 360;

        if (degrees > 46 && degrees <= 134) animancer.Play(dodgeForward);
        if (degrees > 134 && degrees <= 226) animancer.Play(dodgeLeft);
        if (degrees > 226 && degrees <= 314) animancer.Play(dodgeBack);
        if ((degrees > 314 && degrees <= 360) || (degrees >= 0 && degrees <= 46)) animancer.Play(dodgeRight);

    }

    private void PlayerStateManager_OnLeftGround()
    {
        animancer.Play(falling, DEFAULT_FADE_DURATION);
    }

    private void PlayerStateManager_OnAttack2()
    {
        animancer.Play(attack2);
    }

    private void PlayerStateManager_OnDefaultState()
    {
        animancer.Play(strafeTransitionAsset, DEFAULT_FADE_DURATION);
    }

    private void PlayerStateManager_OnLand()
    {
        animancer.Play(land);
    }

    [Sirenix.OdinInspector.Button]
    public void ReachTop()
    {
        animancer.Play(ladderReachTop, MiscConstants.DEFAULT_ANIMATION_BLEND_TIME);
    }

    private void PlayRootMotionAnimation(AnimationClip clip)
    {
        prevRbInterpolation = characterRb.interpolation;
        characterRb.interpolation = RigidbodyInterpolation.None;
        if (rootMotionRedirector == null)
        {
            rootMotionRedirector = animator.gameObject.AddComponent<RedirectRootMotionToTransform>();
        }
        AnimancerState state = animancer.Play(clip);
        state.Events(this).OnEnd ??= OnRootMotionAnimationEnd;
    }

    private void OnRootMotionAnimationEnd()
    {
        characterRb.interpolation = prevRbInterpolation;
        Destroy(rootMotionRedirector);
        rootMotionRedirector = null;
    }
}
