using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using Animancer;
using System.Linq;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private ECM2CharacterAdapter adapter;
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

    private SmoothedVector2Parameter smoothedParameters;
    private MovementAnimationType movementAnimationType;

    private readonly float DEFAULT_FADE_DURATION = 0.1f;

    private enum MovementAnimationType
    {
        ForwardOnly, //only animate between idle, walk forward, and run forward (assumes character's body is always facing movement direction)
        Strafe //forward, backward, strafe, and in-between animations (for when character's body doesn't necessarily face movement direction)
    }

    private void Awake()
    {
        movementAnimationType = MovementAnimationType.ForwardOnly;
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
    }

    private void Update()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(inputVec);

        float xComponent = movementAnimationType == MovementAnimationType.ForwardOnly ? 0 : squareVec.x;
        float yComponent = movementAnimationType == MovementAnimationType.ForwardOnly ? squareVec.magnitude : squareVec.y;
        smoothedParameters.TargetValue = new Vector2(xComponent, yComponent);
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
}
