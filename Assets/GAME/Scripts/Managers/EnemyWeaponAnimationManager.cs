using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAnimationManager : MonoBehaviour
{
    public void StartDamage()
    {
        GetComponentInChildren<Weapon>().StartDamage();
    }

    public void EndDamage()
    {
        GetComponentInChildren<Weapon>().EndDamage(); 
    }
}
