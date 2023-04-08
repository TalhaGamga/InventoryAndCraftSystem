using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateType
{
    Idle,
    Movement,
    Combat
}
public enum PlayerStateType
{
    Movement,
    Combat,
    UIControl
}
public abstract class CharacterStateBase<T>
{
    public abstract void EnterState(T characterStateManager);

    public abstract void UpdateState(T characterStateManager);

    public abstract void ExitState(T characterStateManager);
}