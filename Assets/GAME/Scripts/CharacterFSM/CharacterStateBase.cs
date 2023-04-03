using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterStateType
{
    NeutralState,
    CombatState
}
public abstract class CharacterStateBase : ScriptableObject
{
    public abstract void EnterState(CharacterStateManager characterStateManager);

    public abstract void UpdateState(CharacterStateManager characterStateManager);

    public abstract void ExitState(CharacterStateManager characterStateManager);
}