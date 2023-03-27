using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/EquipmentSystem")]
public class EquipmentSystem : ScriptableObject
{
    public int size;
    public EquipmentSlot[] equipmentSlots;

    public Action<EquipmentSlot> OnEquipmentSlotChanged;

    public void Init()
    {
        equipmentSlots = new EquipmentSlot[size];

        for (int i = 0; i < size; i++)
        {
            equipmentSlots[i] = new EquipmentSlot();
        }
    }

    public bool EquipItem(EquipmentItemData itemData, EquipmentSlot slot)
    {
        if (slot.AssignItem(itemData))
        {
            OnEquipmentSlotChanged?.Invoke(slot);
            return true; 
        }

        return false;
    }
}
