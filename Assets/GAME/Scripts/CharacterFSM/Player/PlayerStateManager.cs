using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] Transform thirdPersonCamera;
    [SerializeField] GameObject avatar;

    CharacterStateBase<PlayerStateManager> currentState;

    MovementState movementState;
    CombatState combatState;
    UIControlState uiControlState;

    PlayerStateType currentStateType = PlayerStateType.Movement;

    ThirdPersonMovementLogic movementLogic;
    [SerializeField] EquipmentSlotUI weaponSlot;

    private void Awake()
    {
        movementLogic = new ThirdPersonMovementLogic(thirdPersonCamera, GetComponent<CharacterController>(), avatar, GetComponent<PlayerInput>(), GetComponent<Animator>());
        movementState = new MovementState(movementLogic, GetComponent<Animator>(), GetComponent<PlayerInput>());
        combatState = new CombatState(GetComponent<PlayerInput>(), movementLogic, GetComponent<Animator>());
        uiControlState = new UIControlState();
    }

    private void OnEnable()
    {
        PlayerInput.onSwitchState += SwitchStateBetweenAttackAndMovement;

        PlayerInput.onAttackInput += SwitchToCombatState;
    }

    private void OnDisable()
    {
        PlayerInput.onSwitchState -= SwitchStateBetweenAttackAndMovement;

        PlayerInput.onAttackInput -= SwitchToCombatState;

        EventManager.OnSwitchToUIControllerState -= SwitchToUIControllerState;
    }

    private void Start()
    {
        currentState = movementState;
        currentState.EnterState(this);

        EventManager.OnSwitchToUIControllerState += SwitchToUIControllerState;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchStateBetweenAttackAndMovement()
    {
        currentState.ExitState(this);

        switch (currentStateType)
        {
            case PlayerStateType.Movement:
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = PlayerStateType.Combat;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }
                break;

            case PlayerStateType.Combat:
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = PlayerStateType.Movement;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;
        }
    }

    public void SwitchState(PlayerStateType switchToState)
    {
        currentState.ExitState(this);


        switch (switchToState)
        {
            case PlayerStateType.Movement: // Switch to movement state
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = PlayerStateType.Movement;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;

            case PlayerStateType.Combat: // Switch to combat state
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = PlayerStateType.Combat;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }

                break;

            case PlayerStateType.UIControl: // Switch to ui controller state
                PlayerStateType formerStateType = currentStateType;
                currentState = uiControlState;
                currentState.EnterState(this);
                ((UIControlState)currentState).GetFormerState(formerStateType, this);

                currentStateType = PlayerStateType.UIControl;

                PlayerInput.onAttackInput -= SwitchToCombatState;

                break;
        }
    }

    public void SwitchToCombatState()
    {
        SwitchState(PlayerStateType.Combat);
    }

    public void SwitchToUIControllerState()
    {
        SwitchState(PlayerStateType.UIControl);
    }
}