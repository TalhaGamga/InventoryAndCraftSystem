using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[CreateAssetMenu(menuName = "Inventory/InventorySystem")]
public class InventorySystem : ScriptableObject
{
    [SerializeField] private int inventorySize;

    public List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;


    public void Init()
    {
        inventorySlots = new List<InventorySlot>(inventorySize);

        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemData, int amountToAdd)
    {
        if (ContainsItem(itemData, out InventorySlot availableSlot))
        {
            availableSlot.AddToStack(amountToAdd);

            OnInventorySlotChanged?.Invoke(availableSlot);

            return true;
        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateSlot(itemData,amountToAdd);

            OnInventorySlotChanged?.Invoke(freeSlot);

            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToCheck, out InventorySlot availableSlot)
    {
        availableSlot = InventorySlots.FirstOrDefault(s => s.ItemData == itemToCheck &&
        s.StackSize < s.ItemData.MaxStackSize);

        return availableSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(s => s.ItemData == null);

        return freeSlot == null ? false : true;
    }
}
