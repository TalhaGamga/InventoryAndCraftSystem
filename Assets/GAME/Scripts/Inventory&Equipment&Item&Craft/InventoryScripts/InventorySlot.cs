using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }

    public InventorySlot()
    {

    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = 0;
    }

    public void AssignItem(InventorySlot invSlot)
    { 
        if (ItemData == invSlot.ItemData)
        {
            AddToStack(invSlot.StackSize);
            return;
        }

        itemData = invSlot.ItemData;
        stackSize = 0;
        AddToStack(invSlot.StackSize);
    }

    public void UpdateSlot(InventoryItemData itemData, int amountToAdd)
    {
        this.itemData = itemData;
        stackSize = 0;
        AddToStack(amountToAdd);
    }

    public void AddToStack(int amount)
    {
        this.stackSize += amount;
    }

    public void RemoveFromStack(int stackToRemove)
    {
        stackSize -= stackToRemove;
    }

    public bool IsSlotEmpty()
    {
        if (StackSize <= 0)
        {
            ClearSlot();
            return true;
        }

        return false;
    }

    public bool IsSlotFull(int stackToAdd, out int remainingStackSize)
    {
        remainingStackSize = itemData.MaxStackSize - stackToAdd;

        if (itemData == null || itemData != null && remainingStackSize > 0) return false;

        return false;
    }

    public bool GetAvailableSize(int stackToAdd, out int availableSize)
    {
        availableSize = ItemData.MaxStackSize - StackSize;

        if (availableSize > 0)
        {
            if (availableSize > stackToAdd)
            {
                availableSize = stackToAdd;
            }

            return true;
        }

        return false;
    }

    public bool SplitSlotStack(out InventorySlot splitSlot)
    {
        if (StackSize <= 1)
        {
            splitSlot = null;
            return false;
        }

        int splitStack = Mathf.RoundToInt(StackSize / 2);

        RemoveFromStack(splitStack);

        splitSlot = new InventorySlot(ItemData, splitStack);

        return true;
    }
}
