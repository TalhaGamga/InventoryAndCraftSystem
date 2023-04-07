using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStateManager : MonoBehaviour
{
    [SerializeField] Transform thirdPersonCamera;
    [SerializeField] GameObject avatar;

    CharacterStateBase currentState;

    MovementState movementState;
    CombatState combatState;
    UIControlState uiControlState;

    CharacterStateType currentStateType = CharacterStateType.Movement;

    ThirdPersonMovementLogic movementLogic;
    [SerializeField] EquipmentSlotUI weaponSlot; //this should be different.
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

    private void Awake()
    {
        movementLogic = new ThirdPersonMovementLogic(thirdPersonCamera, GetComponent<CharacterController>(), avatar, GetComponent<PlayerInput>() ,GetComponent<Animator>());
        movementState = new MovementState(movementLogic, GetComponent<Animator>(), GetComponent<PlayerInput>());
        combatState = new CombatState(GetComponent<PlayerInput>(), movementLogic, GetComponent<Animator>());
        uiControlState = new UIControlState();
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
            case CharacterStateType.Movement:
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = CharacterStateType.Combat;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }   
                break;

            case CharacterStateType.Combat:
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = CharacterStateType.Movement;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;
        }
    }

    public void SwitchState(CharacterStateType switchToState)
    {
        currentState.ExitState(this);
        

        switch (switchToState)
        {
            case CharacterStateType.Movement: // Switch to movement state
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = CharacterStateType.Movement;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;

            case CharacterStateType.Combat: // Switch to combat state
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = CharacterStateType.Combat;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }

                break;

            case CharacterStateType.UIControl: // Switch to ui controller state
                CharacterStateType formerStateType = currentStateType;
                currentState = uiControlState;
                currentState.EnterState(this);
                ((UIControlState)currentState).GetFormerState(formerStateType, this);

                currentStateType = CharacterStateType.UIControl;

                PlayerInput.onAttackInput -= SwitchToCombatState;

                break;
        }
    }

    public void SwitchToCombatState()
    {
        SwitchState(CharacterStateType.Combat);
    }

    public void SwitchToUIControllerState()
    {
        SwitchState(CharacterStateType.UIControl);
    }
}