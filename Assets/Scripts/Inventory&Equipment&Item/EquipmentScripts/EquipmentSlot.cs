using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentSlot
{
    public EquipmentItemData itemData;

    [SerializeField] BodyPart bodyPart;

    [SerializeField] Transform attachTransform;
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
        if (bodyPart == itemData.bodyPart)
        {
            this.itemData = itemData;

            itemData.Attach(attachTransform);

            return true;
        }

        return false;
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

    public void AssignBodyPart(BodyPart bodyPart)
    {
        this.bodyPart = bodyPart;
    }
}
