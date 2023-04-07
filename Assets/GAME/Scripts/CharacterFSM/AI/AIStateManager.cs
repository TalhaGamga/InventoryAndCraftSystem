using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    private const float TURN_SMOOTH_TIME = 0.05f;
    private float turnSmoothVelocity;

    float idleScanRadius = 10;
    float combatScanRadius = 1;
    [SerializeField] Transform scanPoint;
    [SerializeField] LayerMask scanLayer;
    [SerializeField] GameObject avatar;
    [SerializeField] Collider[] scannedColliders = new Collider[10];
    Animator animator;

    AIIdleState aIIdleState;
    AIMovementState aIMovementState;
    AICombatState aICombatState;
    
    AIStateBase currentState;

     
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Idle state
        Gizmos.DrawWireSphere(scanPoint.position, idleScanRadius);

        Gizmos.color = Color.green; // Movement state 
        //Gizmos.DrawWireSphere(scanPoint.position, movementScanRadius);

        Gizmos.color = Color.red; // combat state
        Gizmos.DrawWireSphere(scanPoint.position, combatScanRadius);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        aIIdleState = new AIIdleState(scanPoint, idleScanRadius, scanLayer, scannedColliders);

        aIMovementState = new AIMovementState(scanPoint, idleScanRadius, combatScanRadius, scanLayer, scannedColliders, GetComponent<CharacterController>(),avatar);

        aICombatState = new AICombatState(scanPoint, combatScanRadius, ref aIMovementState.OnTargetDetected,animator);
    }

    private void Start()
    {
        currentState = aIIdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AIStateType stateType)
    {
        switch (stateType)
        {
            case AIStateType.Idle:
                currentState.ExitState(this);
                currentState = aIIdleState;
                currentState.EnterState(this);
                break;

            case AIStateType.Movement:
                currentState.ExitState(this);
                currentState = aIMovementState;
                currentState.EnterState(this);
                break;

            case AIStateType.Combat:
                currentState.ExitState(this);
                currentState = aICombatState;
                currentState.EnterState(this);
                break;
        }
    }

    public void UpdateMovementAnimation(float movementSpeed)
    {
        animator.SetFloat("MoveSpeed", movementSpeed);
    }

    public void RotateAvatarTowardsMoveDirection(Vector3 moveDirection)
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + avatar.transform.rotation.y;
        float angle = Mathf.SmoothDampAngle(avatar.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TURN_SMOOTH_TIME);
        avatar.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

}
