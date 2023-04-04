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

/*  ++++
    Movement logici biyere koy ve di�er scriptlerden onu �ek
    speed parametresine g�re animator karakteri y�nlendirsin
 */

/*
    Sadece Attack ve Movement statede iken UIControl state'e ge�ebilecek bir event tan�mla
*/