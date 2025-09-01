using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Components")] private AnimancerComponent animancer;
    [SerializeField, FoldoutGroup("Components")] private PlayerStateManager playerStateManager;
    [SerializeField, FoldoutGroup("Components")] private Rigidbody characterRb;
    [SerializeField, FoldoutGroup("Components")] private PlayerDefaultMovementModule defaultMovementModule;
    [SerializeField, FoldoutGroup("Components")] private PlayerClimbingModule climbingModule;
    [SerializeField, FoldoutGroup("Components")] private Animator animator;
    [SerializeField, FoldoutGroup("Components")] private Transform cameraFollow;

    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO idle;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO moving;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO attack1;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO attack2;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO jump;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO falling;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO roll;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO dodge;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO landing;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO ladderIdle;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO ladderReachTop;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO ladderStart;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO swordSpin;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO swordUpSlash;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO swordDownSlash;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO ledgeHang;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO ledgeJumpUp;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO bowDraw;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO bowHold;
    [SerializeField, FoldoutGroup("Commands")] private PlayerAnimationCommandSO chestSmallOpen;

    [SerializeField, FoldoutGroup("Clips")] private AnimationClip ladderClimbUpLeftHand;
    [SerializeField, FoldoutGroup("Clips")] private AnimationClip ladderClimbUpRightHand;
    [SerializeField, FoldoutGroup("Clips")] private AnimationClip dodgeForward;
    [SerializeField, FoldoutGroup("Clips")] private AnimationClip dodgeLeft;
    [SerializeField, FoldoutGroup("Clips")] private AnimationClip dodgeBack;
    [SerializeField, FoldoutGroup("Clips")] private AnimationClip dodgeRight;

    [SerializeField, FoldoutGroup("Parameters")] private StringAsset strafeParameterX;
    [SerializeField, FoldoutGroup("Parameters")] private StringAsset strafeParameterY;
    [SerializeField, FoldoutGroup("Parameters")] private StringAsset bowStrafeParameterX;
    [SerializeField, FoldoutGroup("Parameters")] private StringAsset bowStrafeParameterY;

    [SerializeField, FoldoutGroup("Masks")] private AvatarMask torsoMask;

    [SerializeField, FoldoutGroup("Mixers")] private LinearMixerTransition bowMixer;

    private Dictionary<string, PlayerAnimationCommandSO> commands;
    private PlayerAnimationCommandSO currentCommand;
    private SmoothedVector2Parameter strafeSmoothedParameters;
    private SmoothedVector2Parameter bowSmoothedParameters;
    private RigidbodyInterpolation prevRbInterpolation; //stored when we turn off interpolation at the start of a root motion animation, so we can put the RB back to the way it was afterward
    private RedirectRootMotionToTransform rootMotionRedirector;

    private readonly float bowLookMult = -0.006235f;
    private readonly float bowLookAdd = 0.5f;

    public enum SpecialFunction
    {
        None,
        Dodge,
        BowAim
    }

    public enum UpdateType
    {
        None,
        Strafe,
        Bow
    }

    public void Initialize()
    {
        commands = new Dictionary<string, PlayerAnimationCommandSO>()
        {
            { PlayerStateManager.IDLE_STATE, idle },
            { PlayerStateManager.MOVING_STATE, moving },
            { PlayerStateManager.ATTACK_STATE, attack1 },
            { PlayerStateManager.ATTACK2_STATE, attack2 },
            { PlayerStateManager.JUMPING_STATE, jump },
            { PlayerStateManager.DODGING_STATE, dodge },
            { PlayerStateManager.LANDING_STATE, landing },
            { PlayerStateManager.FALLING_STATE, falling },
            { PlayerStateManager.ROLL_STATE, roll },
            { PlayerStateManager.CLIMBING_STATE, ladderIdle },
            { PlayerStateManager.CLIMBING_START_STATE, ladderStart },
            { PlayerStateManager.CLIMBING_REACH_TOP_STATE, ladderReachTop },
            { PlayerStateManager.SWORD_SPIN_STATE, swordSpin },
            { PlayerStateManager.SWORD_UP_SLASH_STATE, swordUpSlash },
            { PlayerStateManager.SWORD_DOWN_SLASH_STATE, swordDownSlash },
            { PlayerStateManager.LEDGE_HANG_STATE, ledgeHang },
            { PlayerStateManager.LEDGE_JUMP_UP_STATE, ledgeJumpUp },
            { PlayerStateManager.BOW_DRAW_STATE, bowDraw },
            { PlayerStateManager.BOW_HOLD_STATE, bowHold },
            { PlayerStateManager.LOOT_STATE, chestSmallOpen },
        };

        strafeSmoothedParameters = new SmoothedVector2Parameter(
            animancer,
            strafeParameterX,
            strafeParameterY,
            0);
        bowSmoothedParameters = new SmoothedVector2Parameter(
            animancer,
            bowStrafeParameterX,
            bowStrafeParameterY,
            0);

        animancer.Layers[1].Mask = torsoMask;

        playerStateManager.OnStateChange += PlayerStateManager_OnStateChange;
        climbingModule.OnLeftHandMoveUp += Climbing_LeftHandMoveUp;
        climbingModule.OnRightHandMoveUp += Climbing_RightHandMoveUp;
    }

    private void OnDisable()
    {
        playerStateManager.OnStateChange -= PlayerStateManager_OnStateChange;

        climbingModule.OnLeftHandMoveUp -= Climbing_LeftHandMoveUp;
        climbingModule.OnRightHandMoveUp -= Climbing_RightHandMoveUp;
    }

    private void Climbing_LeftHandMoveUp()
    {
        animancer.Play(ladderClimbUpLeftHand);
    }

    private void Climbing_RightHandMoveUp()
    {
        animancer.Play(ladderClimbUpRightHand);
    }

    private void Update()
    {
        if (currentCommand == null) return;

        if (currentCommand.UpdateType == UpdateType.Strafe) UpdateStrafeAnimation();
        if (currentCommand.UpdateType == UpdateType.Bow) UpdateBowAnimation();
    }

    private void UpdateBowAnimation()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(inputVec);
        bowSmoothedParameters.TargetValue = new Vector2(squareVec.x, squareVec.y);

        float cameraAngleX = cameraFollow.eulerAngles.x;
        if (cameraAngleX > 180) cameraAngleX -= 360;
        if (bowMixer.State != null) bowMixer.State.Parameter = bowLookMult * cameraAngleX + bowLookAdd;
    }

    private void UpdateStrafeAnimation()
    {
        Vector3 inputVec = InputActionsProvider.GetPrimaryAxis();
        Vector3 squareVec = Utils.ApproximateSquareInputVector(inputVec); 

        float xComponent = defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly ? 0 : squareVec.x;
        float yComponent = defaultMovementModule.CurrentMovementType == PlayerDefaultMovementModule.MovementType.ForwardOnly ? squareVec.magnitude : squareVec.y;
        strafeSmoothedParameters.TargetValue = new Vector2(xComponent, yComponent);
    }

    private void PlayerStateManager_OnStateChange(string newState, string prevState)
    {
        if (commands.TryGetValue(newState, out PlayerAnimationCommandSO command))
        {
            RunAnimationCommand(command);
        }
    }

    public void RunAnimationCommand(PlayerAnimationCommandSO command)
    {
        currentCommand = command;

        float fadeDuration = command.UseCustomFadeDuration ? command.FadeDuration : MiscConstants.DEFAULT_ANIMATION_BLEND_TIME;
        if (command.UseRootMotion && command.BaseAnimation != null)
        {
            PlayRootMotionAnimation(command.BaseAnimation);
            return;
        }
        if (command.BaseAnimation != null) animancer.Play(command.BaseAnimation, fadeDuration).Time = 0;
        if (command.BaseTransitionAsset != null) animancer.Play(command.BaseTransitionAsset, fadeDuration);
        if (command.TorsoAnimation != null) animancer.Layers[1].Play(command.TorsoAnimation, fadeDuration);
        if (command.TorsoTransitionAsset != null) animancer.Layers[1].Play(command.TorsoTransitionAsset, fadeDuration).Time = 0;
        animancer.Layers[1].Weight = command.TorsoLayerWeight;
        if (command.SpecialFunction != SpecialFunction.None)
        {
            RunSpecialFunction(command.SpecialFunction);
        }
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

    private void RunSpecialFunction(SpecialFunction func)
    {
        if (func == SpecialFunction.Dodge) PlayDodgeAnimation();
        if (func == SpecialFunction.BowAim) PlayBowAim();
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

    private void PlayBowAim()
    {
        animancer.Layers[1].Play(bowMixer);
    }
}
