using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentSlot
{
    public EquipmentItemData itemData;

    public EquipmentSlot(EquipmentItemData itemData)
    {
        this.itemData = itemData;
    }

    public EquipmentSlot()
    {

    }

    public void ClearSlot()
    {
        itemData = null;
    }

    public bool AssignItem(EquipmentItemData itemData)
    {
        this.itemData = itemData;
        return true;
    }

    public void UpdateSlot(EquipmentItemData itemData) 
    {
        this.itemData = itemData;
    }

    public bool IsSlotEmpty()
    {
        if (itemData == null) return true;

        return false;
    }
}
