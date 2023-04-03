using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private const float TURN_SMOOTH_TIME = 0.05f;

    [SerializeField] Transform playerCam;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float gravity = -18f;
    [SerializeField] float jumpHeight = 3f;

    private CharacterController controller;
    [SerializeField] private GameObject avatar;

    [SerializeField] private float verticalVelocity;
    private float turnSmoothVelocity;
    [SerializeField] public float CurrentMoveSpeed;

    private bool jumpTrigger;
    private bool isRunning;

    [SerializeField] private float groundOffSet = -0.22f;
    [SerializeField] private float groundRadius = 0.28f;
    [SerializeField] private LayerMask groundmask;

    [SerializeField] float moveSpeed;
    private void Awake() 
    {
        controller = GetComponent<CharacterController>();
    }
    public void Setup(GameObject target)
    {
        avatar = target;
    }

    public void Move(float inputX, float inputY)
    {
        Vector3 moveDirection = playerCam.right * inputX + playerCam.forward * inputY;
        moveSpeed = isRunning ? runSpeed : walkSpeed;

        controller.Move(moveDirection.normalized * (moveSpeed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity * Time.deltaTime, 0.0f));

        float moveMagnitude = moveDirection.magnitude;

        JumpAndGravity();
        //CurrentMoveSpeed = isRunning ? runSpeed * moveMagnitude : walkSpeed * moveMagnitude;
        CurrentMoveSpeed = Mathf.Lerp(CurrentMoveSpeed, moveSpeed * moveMagnitude, 0.05f);

        if (moveMagnitude > 0)
        {
            RotateAvatarTowardsMoveDirection(moveDirection);
        }
    }

    private void RotateAvatarTowardsMoveDirection(Vector3 moveDirection)
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + transform.rotation.y;
        float angle = Mathf.SmoothDampAngle(avatar.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TURN_SMOOTH_TIME);
        avatar.transform.rotation = Quaternion.Euler(0, angle, 0);
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

    public void SetIsRunning(bool running)
    {
        isRunning = running;
    }

    public bool TryJump()
    {
        jumpTrigger = false;
        if (controller.isGrounded)
        {
            jumpTrigger = true;
        }
        return jumpTrigger;
    }


    public bool IsGrounded()
    {
        if (verticalVelocity > 0)
        {
            return false;
        }

        Vector3 pos = transform.position;
        Vector3 spherePosition = new Vector3(pos.x, pos.y + groundOffSet, pos.z);

        //return Physics.CheckSphere(spherePosition, groundRadius, groundmask);
        return controller.isGrounded;
    }
}
