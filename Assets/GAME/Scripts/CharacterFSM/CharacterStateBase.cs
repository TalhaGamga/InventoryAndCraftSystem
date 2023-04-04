using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterStateType
{
    MovementState,
    CombatState,
    UIControlState
}
public abstract class CharacterStateBase 
{
    public abstract void EnterState(CharacterStateManager characterStateManager);

    public abstract void UpdateState(CharacterStateManager characterStateManager);

    public abstract void ExitState(CharacterStateManager characterStateManager);
}