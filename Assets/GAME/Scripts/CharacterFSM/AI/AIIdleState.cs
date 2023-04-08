using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : CharacterStateBase<AIStateManager>
{
    float scanIdleRadius;
    LayerMask scanLayer;
    Transform scanPoint;
    Collider[] scannedColliders;

    public AIIdleState(Transform scanPoint, float scanIdleRadius, LayerMask scanLayer, Collider[] scannedColliders)
    {
        this.scanPoint = scanPoint;
        this.scanIdleRadius = scanIdleRadius;
        this.scanLayer = scanLayer;
        this.scannedColliders = scannedColliders;
    }


    public override void EnterState(AIStateManager aIStateManager)
    {
        aIStateManager.UpdateMovementAnimation(0);
    }

    public override void UpdateState(AIStateManager aIStateManager)
    {
        float targetLen = Physics.OverlapSphereNonAlloc(scanPoint.position, scanIdleRadius, scannedColliders, scanLayer);

        if (targetLen > 0)
        {
            aIStateManager.SwitchState(AIStateType.Movement);
        }
    }

    public override void ExitState(AIStateManager aIStateManager)
    {
    }
}