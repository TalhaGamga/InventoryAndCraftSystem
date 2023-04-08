using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIControlState : CharacterStateBase<PlayerStateManager>
{
    PlayerStateType formerStateType;
    public override void EnterState(PlayerStateManager characterStateManager)
    {
        EventManager.OnSwitchToUIControllerState -= characterStateManager.SwitchToUIControllerState;
    }

    public override void UpdateState(PlayerStateManager characterStateManager)
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            characterStateManager.SwitchState(formerStateType);
        } 
    }

    public override void ExitState(PlayerStateManager characterStateManager)
    {
        EventManager.OnSwitchToUIControllerState += characterStateManager.SwitchToUIControllerState;
    }

    public void GetFormerState(PlayerStateType formerStateType, PlayerStateManager characterStateManager)
    {
        this.formerStateType = formerStateType;
    }

}
