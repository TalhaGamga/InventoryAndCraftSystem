using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStateManager : MonoBehaviour
{
    [SerializeField] Transform thirdPersonCamera;
    CharacterController controller;
    [SerializeField] GameObject avatar;
    PlayerInput playerInput;
    [SerializeField] LayerMask groundMask;
    Animator animator;

    CharacterStateBase currentState;

    MovementState movementState;
    CombatState combatState;
    UIControlState uiControlState;

    CharacterStateType currentStateType = CharacterStateType.MovementState;

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
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        movementLogic = new ThirdPersonMovementLogic(thirdPersonCamera, controller, avatar, playerInput, groundMask, animator);

        movementState = new MovementState(movementLogic, animator, playerInput);
        combatState = new CombatState(playerInput, movementLogic, animator);
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
            case CharacterStateType.MovementState:
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = CharacterStateType.CombatState;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }   
                break;

            case CharacterStateType.CombatState:
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = CharacterStateType.MovementState;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;
        }
    }

    public void SwitchState(CharacterStateType switchToState)
    {
        currentState.ExitState(this);

        switch (switchToState)
        {
            case CharacterStateType.MovementState: // Switch to movement state
                currentState = movementState;
                currentState.EnterState(this);

                currentStateType = CharacterStateType.MovementState;

                PlayerInput.onAttackInput += SwitchToCombatState;
                break;

            case CharacterStateType.CombatState: // Switch to combat state
                if (weaponSlot.AssignedEquipmentSlot.itemData != null)
                {
                    currentState = combatState;
                    currentState.EnterState(this);

                    currentStateType = CharacterStateType.CombatState;

                    PlayerInput.onAttackInput -= SwitchToCombatState;
                }

                break;

            case CharacterStateType.UIControlState: // Switch to ui controller state
                CharacterStateType formerStateType = currentStateType;
                currentState = uiControlState;
                currentState.EnterState(this);
                ((UIControlState)currentState).GetFormerState(formerStateType, this);

                currentStateType = CharacterStateType.UIControlState;

                PlayerInput.onAttackInput -= SwitchToCombatState;

                break;
        }
    }

    public void SwitchToCombatState()
    {
        SwitchState(CharacterStateType.CombatState);
    }

    public void SwitchToUIControllerState()
    {
        SwitchState(CharacterStateType.UIControlState);
    }
}