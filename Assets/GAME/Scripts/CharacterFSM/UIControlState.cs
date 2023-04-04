using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIControlState : CharacterStateBase
{
    CharacterStateType formerStateType;
    public override void EnterState(CharacterStateManager characterStateManager)
    {
        EventManager.OnSwitchToUIControllerState -= characterStateManager.SwitchToUIControllerState;
    }

    public override void UpdateState(CharacterStateManager characterStateManager)
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            characterStateManager.SwitchState(formerStateType);
            Debug.Log(formerStateType);
        }
    }

    public override void ExitState(CharacterStateManager characterStateManager)
    {
        EventManager.OnSwitchToUIControllerState += characterStateManager.SwitchToUIControllerState;
    }

    public void GetFormerState(CharacterStateType formerStateType, CharacterStateManager characterStateManager)
    {
        this.formerStateType = formerStateType;
    }

}
