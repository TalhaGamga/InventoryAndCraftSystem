using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : CharacterStateBase
{
    Animator animator;

    float moveSpeed = 3;

    private PlayerInput playerInput;

    ThirdPersonMovementLogic movementLogic;

    public CombatState(PlayerInput playerInput, ThirdPersonMovementLogic movementLogic, Animator animator)
    {
        this.playerInput = playerInput;

        this.animator = animator;

        this.movementLogic = movementLogic;
    }

    public override void EnterState(CharacterStateManager characterStateManager)
    {
        animator.SetTrigger("DrawWeapon");


        PlayerInput.onAttackInput += PerformAttack;
    }

    public override void UpdateState(CharacterStateManager characterStateManager)
    {
        playerInput.CheckInput();

        float xInput = playerInput.horizontalKeyInput;
        float yInput = playerInput.verticalKeyInput;

        movementLogic.Move(moveSpeed);

        movementLogic.UpdateAnimator();
    }

    public override void ExitState(CharacterStateManager characterStateManager)
    {

        animator.SetTrigger("SheathWeapon");


        PlayerInput.onAttackInput -= PerformAttack;
    }

    private void PerformAttack()
    {
        animator.SetTrigger("Attack");
    }
}