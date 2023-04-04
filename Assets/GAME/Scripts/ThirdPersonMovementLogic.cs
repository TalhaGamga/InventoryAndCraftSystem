using System;
using UnityEngine;

public class ThirdPersonMovementLogic
{
    private const float FALL_TIMEOUT = 0.15f;

    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private static readonly int JumpHash = Animator.StringToHash("JumpTrigger");
    private static readonly int FreeFallHash = Animator.StringToHash("FreeFall");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

    public Animator animator; // Avatar's animator
    private GameObject avatar; // Avatar obj

    private PlayerInput playerInput; // PlayerInput component on our parent obj

    private float fallTimeoutDelta; // Falling delta time

    private const float TURN_SMOOTH_TIME = 0.05f;

    private Transform playerCam; // Third person camera's transform
    //private float walkSpeed = 3f; // Walk speed
    //private float runSpeed = 8f; // Run speed
    private float gravity = -18f; // Gravity
    private float jumpHeight = 3f; //Jump height

    private CharacterController controller; // Character controller coming from monobehaviour

    private float verticalVelocity;
    private float turnSmoothVelocity; //turn speed simultaneously with camera look angle
    public float CurrentMoveSpeed;

    private bool jumpTrigger;


    private float groundOffSet = -0.22f;
    private float groundRadius = 0.28f;
    LayerMask groundMask;

    private float moveSpeed;


    public ThirdPersonMovementLogic(Transform camera, CharacterController controller, GameObject avatar, PlayerInput playerInput, LayerMask groundMask,Animator animator)
    {
        playerCam = camera;
        this.controller = controller;
        this.avatar = avatar;
        this.playerInput = playerInput;
        this.groundMask = groundMask;

        this.animator = animator;
    }

    public void Move(float speed)
    {
        if (avatar == null)
        {
            return;
        }

        float xAxisKeyInput = playerInput.horizontalKeyInput;
        float yAxisKeyInput = playerInput.verticalKeyInput;

        Vector3 moveDirection = playerCam.right * xAxisKeyInput + playerCam.forward * yAxisKeyInput;

        moveSpeed = speed;

        controller.Move(moveDirection.normalized * (moveSpeed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity * Time.deltaTime, 0.0f));

        float moveMagnitude = moveDirection.magnitude;

        JumpAndGravity();
        CurrentMoveSpeed = Mathf.Lerp(CurrentMoveSpeed, moveSpeed * moveMagnitude, 0.05f);

        if (moveMagnitude > 0)
        {
            RotateAvatarTowardsMoveDirection(moveDirection);
        }
    }

    private void JumpAndGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (jumpTrigger && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpTrigger = false;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void RotateAvatarTowardsMoveDirection(Vector3 moveDirection)
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + avatar.transform.rotation.y;
        float angle = Mathf.SmoothDampAngle(avatar.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TURN_SMOOTH_TIME);
        avatar.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void UpdateAnimator()
    {
        bool isGrounded = IsGrounded();
        animator.SetFloat(MoveSpeedHash, CurrentMoveSpeed);
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

    public void OnJump()
    {
        if (TryJump())
        {
            animator.SetTrigger(JumpHash);
        }
    }

    private bool TryJump()
    {
        jumpTrigger = false;
        if (controller.isGrounded)
        {
            jumpTrigger = true;
        }
        return jumpTrigger;
    }


    private bool IsGrounded()
    {
        if (verticalVelocity > 0)
        {
            return false;
        }

        Vector3 pos = controller.transform.position;
        Vector3 spherePosition = new Vector3(pos.x, pos.y + groundOffSet, pos.z);

        return controller.isGrounded;
    }
}
