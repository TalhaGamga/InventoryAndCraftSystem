using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHP;
    [SerializeField] float currentHp;
    Animator animator;
    void Awake()
    {
        currentHp = maxHP;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        animator.SetTrigger("Damage");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        animator.SetTrigger("Die");
    }
}
