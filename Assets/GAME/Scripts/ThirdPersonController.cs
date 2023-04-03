using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private const float FALL_TIMEOUT = 0.15f;

    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private static readonly int JumpHash = Animator.StringToHash("JumpTrigger");
    private static readonly int FreeFallHash = Animator.StringToHash("FreeFall");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject avatar;
    private ThirdPersonMovement thirdPersonMovement;
    [SerializeField] private PlayerInput playerInput;

    private float fallTimeoutDelta;

    [SerializeField] private bool inputEnabled = true;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnJumpPress += OnJump;
    }

    private void Update()
    {
        if (avatar == null)
        {
            return;
        }

        if (inputEnabled)
        {
            playerInput.CheckInput();
            float xAxisKeyInput = playerInput.horizontalKeyInput;
            float yAxisKeyInput = playerInput.verticalKeyInput;
            thirdPersonMovement.Move(xAxisKeyInput, yAxisKeyInput);
            thirdPersonMovement.SetIsRunning(playerInput.IsHoldingLeftShift);
        }
        UpdateAnimator();
    }
     
    private void UpdateAnimator()
    {
        bool isGrounded = thirdPersonMovement.IsGrounded();
        animator.SetFloat(MoveSpeedHash, thirdPersonMovement.CurrentMoveSpeed);
        animator.SetBool(IsGroundedHash, isGrounded);

        if (isGrounded)
        {
            fallTimeoutDelta = FALL_TIMEOUT;
            animator.SetBool(FreeFallHash, false);
        }
        else
        {
            if (fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                animator.SetBool(FreeFallHash, true);
            }
        }
    }

    private void OnJump()
    {
        if (thirdPersonMovement.TryJump())
        {
            animator.SetTrigger(JumpHash);
        }
    }
}
