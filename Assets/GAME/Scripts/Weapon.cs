using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField] private float swordLength;
    [SerializeField] private LayerMask layer;
    [SerializeField] bool isAttackable = false;
    [SerializeField] bool isAttacked = false;
    [SerializeField] float damage;

    [SerializeField] AnimatorOverrideController overrideController;

    private void Start()
    {
        AnimationSwapSystem.OnChangeAnimatorCombatLayer?.Invoke(overrideController); 
    }

    private void OnDestroy()
    {
        AnimationSwapSystem.OnResetAnimatorCombatLayer?.Invoke();
    }

    private void Update()
    {
        if (isAttackable && !isAttacked)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.up, out hit, swordLength))
            {
                if (hit.transform.TryGetComponent<IDamagable>(out IDamagable damagable) && hit.transform.gameObject != gameObject)
                {
                    damagable.TakeDamage(damage);
                }
            }
        }
    }

    public void StartDamage()
    {
        isAttackable = true;
        isAttacked = false;
    }

    public void EndDamage()
    {
        isAttackable = false;
        isAttacked = true;
    }
}


/*
public Animator characterAnimator;
public AnimationClip newAttackClip;

void ChangeAttackAnimation()
{
    // Get the animator controller for the character
    AnimatorController animatorController = characterAnimator.runtimeAnimatorController as AnimatorController;

    // Get the Combat layer
    AnimatorControllerLayer combatLayer = animatorController.layers.FirstOrDefault(layer => layer.name == "Combat");

    // Get the Attack state
    AnimatorStateMachine combatStateMachine = combatLayer.stateMachine;
    AnimatorState attackState = combatStateMachine.states.FirstOrDefault(state => state.state.name == "Attack").state;

    // Set the new attack clip on the state's motion property
    attackState.motion = newAttackClip;
}
 */