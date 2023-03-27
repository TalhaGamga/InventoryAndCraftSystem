using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentSlot
{
    public EquipmentItemData itemData;

    public BodyPart bodyPart;

    public void ClearSlot()
    {
        itemData = null;
    }

    public bool AssignItem(EquipmentItemData itemData)
    {
        if (bodyPart==itemData.bodyPart)
        {
            this.itemData = itemData;
            return true;
        }

        return false;
    }

    public bool IsSlotEmpty()
    {
        if (itemData == null) return true;

        return false;
    }
}
