using System;
using UnityEngine;
public class MovementState : CharacterStateBase
{
    ThirdPersonMovementLogic movementLogic;

    Animator animator;

    float walkSpeed = 3f;
    float runSpeed = 8f;

    float moveSpeed;

    PlayerInput playerInput;
    public MovementState(ThirdPersonMovementLogic movementLogic, Animator animator, PlayerInput playerInput)
    { 
        this.movementLogic = movementLogic;

        this.animator = animator;

        this.playerInput = playerInput;
    }

    public override void EnterState(CharacterStateManager characterStateManager)
    {
        playerInput.OnJumpPress += movementLogic.OnJump;
    }

    public override void UpdateState(CharacterStateManager characterStateManager)
    {
        playerInput.CheckInput();

        bool isRunning = SetIsRunning();
        moveSpeed = isRunning ? runSpeed : walkSpeed;


        float xInput = playerInput.horizontalKeyInput;
        float yInput = playerInput.verticalKeyInput;

        movementLogic.Move(moveSpeed);

        movementLogic.UpdateAnimator();
    }

    public override void ExitState(CharacterStateManager characterStateManager)
    {
        playerInput.OnJumpPress -= movementLogic.OnJump;
        animator.SetFloat("MoveSpeed", 0);
    }

    private bool SetIsRunning()
    {
        return playerInput.IsHoldingLeftShift;
    }
}