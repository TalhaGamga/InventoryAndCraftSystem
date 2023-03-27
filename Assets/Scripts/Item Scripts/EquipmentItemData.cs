using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/EquipmentItemData")]
public class EquipmentItemData : InventoryItemData
{
    public Vector3 localPosition;
    public Vector3 localRotation;
    public Vector3 localScale;

    public BodyPart bodyPart;
}
