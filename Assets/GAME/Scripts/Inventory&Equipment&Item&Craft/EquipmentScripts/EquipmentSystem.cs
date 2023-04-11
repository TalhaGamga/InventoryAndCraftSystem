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
}
