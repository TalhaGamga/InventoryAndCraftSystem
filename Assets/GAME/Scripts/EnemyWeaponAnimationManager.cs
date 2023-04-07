using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAnimationManager : MonoBehaviour
{
    public void StartDamage()
    {
        GetComponentInChildren<SwordDamageManager>().StartDamage();
    }

    public void EndDamage()
    {
        GetComponentInChildren<SwordDamageManager>().EndDamage();
    }
}
