using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/EquipmentItemData")]
public class EquipmentItemData : InventoryItemData
{
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;

    public BodyPart bodyPart;

    public GameObject itemPrefab;

    GameObject item;
    public void Attach(Transform parent, BodyPart bodyPart)
    {
        if (this.bodyPart == bodyPart)
        {
            item = Instantiate(itemPrefab, localPosition, Quaternion.identity, parent);
            item.transform.localRotation = localRotation;
            item.transform.localScale = localScale;
        }
    }

    public void Remove()
    {
        Destroy(item);
    }
}
