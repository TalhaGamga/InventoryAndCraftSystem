using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombatState : CharacterStateBase<AIStateManager>
{
    private const float TURN_SMOOTH_TIME = 0.05f;

    Transform scanPoint;
    float combatRadius;
    GameObject target;
    Animator animator;
    GameObject avatar;

    public AICombatState(Transform scanPoint, float combatRadius, ref Action<GameObject> onTargetDetected, Animator animator)
    {
        this.scanPoint = scanPoint;
        this.combatRadius = combatRadius;
        onTargetDetected += SetTarget;
        this.animator = animator;
    }

    public override void EnterState(AIStateManager aIStateManager)
    {
        animator.SetBool("Attack", true);
    }

    public override void UpdateState(AIStateManager aIStateManager)
    {
        if (!target) return;

        if (Vector3.Distance(scanPoint.position, target.transform.position) >= combatRadius*2) aIStateManager.SwitchState(AIStateType.Movement);
        Vector3 direction = (target.transform.position - scanPoint.position).normalized;
        aIStateManager.RotateAvatarTowardsMoveDirection(direction);
    }

    public override void ExitState(AIStateManager aIStateManager)
    {
        animator.SetBool("Attack", false);
    }

    private void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
