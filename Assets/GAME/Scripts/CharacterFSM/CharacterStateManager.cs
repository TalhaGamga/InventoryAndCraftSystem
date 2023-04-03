using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterStateManager : MonoBehaviour
{
    CharacterStateBase currentState;

    [SerializeField] CombatState combatState;
    [SerializeField] NeutralState neutralState;

    public Animator anim;

    CharacterStateType currentStateType = CharacterStateType.NeutralState;

    private void OnEnable()
    {
        PlayerInput.onSwitchState += SwitchState;
    }

    private void OnDisable()
    {
        PlayerInput.onSwitchState -= SwitchState;
    }

    private void Start()
    {
        currentState = neutralState;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState()
    {
        currentState.ExitState(this);

        switch (currentStateType)
        {
            case CharacterStateType.NeutralState:
                currentState = combatState;
                currentState.EnterState(this);
                currentStateType = CharacterStateType.CombatState;
                break;

            case CharacterStateType.CombatState:
                currentState = neutralState;
                currentStateType = CharacterStateType.NeutralState;
                currentState.EnterState(this);
                break;
        }
    }
}