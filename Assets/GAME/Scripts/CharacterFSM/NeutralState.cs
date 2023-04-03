using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterState/NeutralState")]
public class NeutralState : CharacterStateBase
{
    public override void EnterState(CharacterStateManager characterStateManager)
    {
        characterStateManager.anim.SetTrigger("SheathWeapon");
    }

    public override void UpdateState(CharacterStateManager characterStateManager)
    {
        //throw new System.NotImplementedException();
    }

    public override void ExitState(CharacterStateManager characterStateManager)
    {
        //throw new System.NotImplementedException();
    }
}
