using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementState : AIStateBase
{

    Transform scanPoint;
    float chaseRadius;
    float combatRadius;
    LayerMask scanLayer;
    Collider[] scannedColliders;
    CharacterController controller;
    GameObject avatar;
    public Action<GameObject> OnTargetDetected;
    float speed = 5f;
    float moveSpeed;

    public AIMovementState(Transform scanPoint, float chaseRadius, float combatRadius, LayerMask scanLayer, Collider[] scannedColliders, CharacterController controller, GameObject avatar)
    {
        this.scanPoint = scanPoint;
        this.chaseRadius = chaseRadius;
        this.combatRadius = combatRadius;
        this.scanLayer = scanLayer;
        this.scannedColliders = scannedColliders;
        this.controller = controller;
        this.avatar = avatar;
    }

    public override void EnterState(AIStateManager aIStateManager)
    {
    }

    public override void UpdateState(AIStateManager aIStateManager)
    {
        float targetLen = Physics.OverlapSphereNonAlloc(scanPoint.position, chaseRadius, scannedColliders, scanLayer);

        if (targetLen > 0)
        {
            Collider target = scannedColliders[0];

            for (int i = 0; i < targetLen; i++)
            {
                Collider tempCollider = scannedColliders[i];

                if (Vector3.Distance(scanPoint.position, target.transform.position) >= Vector3.Distance(scanPoint.position, tempCollider.transform.position))
                {
                    target = tempCollider;

                    Vector3 moveDirection = (target.transform.position - scanPoint.position).normalized;

                    moveSpeed = Mathf.Lerp(moveSpeed, speed, .5f);

                    controller.Move(moveDirection * moveSpeed * Time.deltaTime);

                    aIStateManager.UpdateMovementAnimation(moveSpeed);

                    aIStateManager.RotateAvatarTowardsMoveDirection(moveDirection);

                    if (Vector3.Distance(scanPoint.position, target.transform.position) <= combatRadius)
                    {
                        OnTargetDetected?.Invoke(target.gameObject);

                        aIStateManager.SwitchState(AIStateType.Combat);
                    }
                }
            }
        }

        else
        {
            aIStateManager.SwitchState(AIStateType.Idle);
        }
    }
    public override void ExitState(AIStateManager aIStateManager)
    {
    }
}
