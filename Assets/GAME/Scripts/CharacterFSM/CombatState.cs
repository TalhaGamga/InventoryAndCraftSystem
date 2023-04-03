using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "CharacterState/CombatState")]
public class CombatState : CharacterStateBase
{
    Animator anim;
    public override void EnterState(CharacterStateManager characterStateManager)
    {
        this.anim = characterStateManager.anim;
        anim.SetTrigger("DrawWeapon");
        PlayerInput.onAttackInput += PerformAttack;
    }

    public override void UpdateState(CharacterStateManager characterStateManager)
    {
        //PerformAttack();
    }

    public override void ExitState(CharacterStateManager characterStateManager)
    {
        PlayerInput.onAttackInput -= PerformAttack;
    }

    private void PerformAttack()
    {
        anim.SetTrigger("Attack"); 
    }
}
