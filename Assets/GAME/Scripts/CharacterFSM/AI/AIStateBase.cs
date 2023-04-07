using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateType
{
    Idle,
    Movement,
    Combat
}

public abstract class AIStateBase
{
    public abstract void EnterState(AIStateManager aIStateManager);

    public abstract void UpdateState(AIStateManager aIStateManager);

    public abstract void ExitState(AIStateManager aIStateManager);
}