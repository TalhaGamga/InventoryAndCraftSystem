using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSheathManager : MonoBehaviour
{
    [SerializeField] EquipmentSlotUI weaponEquipmentSlot;
    [SerializeField] Transform takeParent;
    [SerializeField] Vector3 takePos;
    [SerializeField] Quaternion takeRot;

    GameObject item;
    public void TakeItem()
    {
        if (weaponEquipmentSlot.AssignedEquipmentSlot.itemData != null)
        {
            item = weaponEquipmentSlot.AssignedEquipmentSlot.itemData.item;

            item.transform.SetParent(takeParent);
            item.transform.localPosition = takePos;
            item.transform.localRotation = takeRot;
        }
    }

    public void PutItem()
    {
        if (weaponEquipmentSlot.AssignedEquipmentSlot.itemData != null)
        {
            weaponEquipmentSlot.AssignedEquipmentSlot.itemData.Attach(weaponEquipmentSlot.attachTransform);
        }
    }
}
