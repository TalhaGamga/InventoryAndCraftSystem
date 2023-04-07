using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageManager : MonoBehaviour
{
    [SerializeField] private float swordLength;
    [SerializeField] private LayerMask layer;
    [SerializeField] bool isAttackable = false;
    [SerializeField] bool isAttacked = false;
    [SerializeField] float damage;

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
