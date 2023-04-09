using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationSwapSystem : MonoBehaviour
{
    Animator playerAnimator;

    public static Action<Animator> OnChangeAnimatorCombatLayer;
    public static Action OnResetAnimatorCombatLayer;

    [SerializeField] private AnimatorOverrideController defaultAnimator;
    private void OnEnable()
    {
        OnChangeAnimatorCombatLayer += ChangeAnimatorCombatLayer;
        OnResetAnimatorCombatLayer += ResetAnimatorCombatLayer;
    }

    private void OnDisable()
    {
        OnChangeAnimatorCombatLayer -= ChangeAnimatorCombatLayer;
        OnResetAnimatorCombatLayer -= ResetAnimatorCombatLayer;
    }

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void ChangeAnimatorCombatLayer(Animator overrideAnimator)
    {
        playerAnimator.runtimeAnimatorController = overrideAnimator.runtimeAnimatorController;
    }

    public void ResetAnimatorCombatLayer()
    {
        playerAnimator.runtimeAnimatorController = defaultAnimator;
    }
}