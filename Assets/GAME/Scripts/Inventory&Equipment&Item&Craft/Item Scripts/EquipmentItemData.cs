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

    [HideInInspector] public GameObject item;

    public void Attach(Transform parent)
    {
        if (item==null)
        {
            item = Instantiate(itemPrefab);
        }

        item.transform.SetParent(parent);
        item.transform.localPosition = localPosition;
        item.transform.localRotation = localRotation;
        item.transform.localScale = localScale;
    }

    public void RemoveItem()
    {
        Destroy(item);
    }
}
