using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 1f;
    [SerializeField] private float mouseSesitivityY = 1f;

    public float horizontalKeyInput;
    public float verticalKeyInput;

    [HideInInspector] public float horizontalMouseInput;
    [HideInInspector] public float verticalMouseInput;

    PlayerControls playerControls;

    private Vector2 mouseMovement;

    public Action OnJumpPress;

    [SerializeField] Vector2 delta;

    public bool IsHoldingLeftShift => Keyboard.current.leftShiftKey.isPressed;

    public delegate void OnAttackInput();
    public static event OnAttackInput onAttackInput;

    public delegate void OnSwitchState();
    public static event OnSwitchState onSwitchState;

    private InputAction switchStateAction;
    private InputAction attackAction;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls?.Enable();
    }

    private void Start()
    {
        switchStateAction = new InputAction("SwitchState", InputActionType.Button, "<Keyboard>/R");
        switchStateAction?.Enable();

        attackAction = new InputAction("Attack", InputActionType.Button, "<Mouse>/LeftButton");
        attackAction?.Enable();
    }

    public void CheckInput()
    {
        horizontalKeyInput = playerControls.Player.Movement.ReadValue<Vector2>().x;
        verticalKeyInput = playerControls.Player.Movement.ReadValue<Vector2>().y;

        delta = Mouse.current.delta.ReadValue() * .1f;

        horizontalMouseInput = delta.x * mouseSensitivityX;
        verticalMouseInput = delta.y * mouseSesitivityY;

        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            OnJumpPress?.Invoke();
        }

        if (attackAction.triggered) onAttackInput?.Invoke();

        if (switchStateAction.triggered) onSwitchState?.Invoke();
    }
}
