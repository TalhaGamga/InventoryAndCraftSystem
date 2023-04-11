using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CraftingRecipeData")]
public class CraftingRecipeData : ScriptableObject
{
    [SerializeField] InputItem[] inputItems;

    public InventoryItemData resultData;

    public HashSet<InputItem> inputItemSet;

    public void InitData()
    {
        inputItemSet = new HashSet<InputItem>();

        for (int i = 0; i < inputItems.Length; i++)
        {
            inputItemSet.Add(inputItems[i]);
        }
    }
}

[System.Serializable]
public class InputItem : IEquatable<InputItem>
{
    public InventoryItemData itemData;
    public int size;

    public InputItem(InventoryItemData itemData, int size)
    {
        this.itemData = itemData;
        this.size = size;
    }

    public bool Equals(InputItem other)
    {
        if (other == null) return false;
        return (this.itemData.Equals(other.itemData) && this.size == other.size);
    }

    public override int GetHashCode()
    {
        return this.itemData.GetHashCode() ^ this.size;
    }
}
