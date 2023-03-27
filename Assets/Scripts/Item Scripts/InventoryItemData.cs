using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ItemData")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int Value;

    public ItemType itemType;

    public bool Collect(InventoryHolder inventoryHolder)
    {
        return inventoryHolder.AddToInventory(this);
    }
}
