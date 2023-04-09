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
     
    [SerializeField] Animator overrideAnimator;

    private void Start()
    {
        AnimationSwapSystem.OnChangeAnimatorCombatLayer?.Invoke(overrideAnimator); 
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