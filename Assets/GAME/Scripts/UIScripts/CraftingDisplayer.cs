using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDisplayer : MonoBehaviour
{
    [SerializeField] MouseInventorySlotUI mouseInventorySlotUI;

    [SerializeField] CraftingSystem craftingSystem;

    [SerializeField] CraftingSlotUI[] uiSlots;
    [SerializeField] CraftingSlotUI resulterUISlot;

    public Dictionary<CraftingSlotUI, InventorySlot> slotDictionary = new Dictionary<CraftingSlotUI, InventorySlot>();

    private void OnEnable()
    {
        craftingSystem.OnCraftingSlotUpdated += UpdateUISlot;
    }

    private void OnDisable()
    {
        craftingSystem.OnCraftingSlotUpdated += UpdateUISlot;
    }

    private void Start()
    {
        AssignSlots();
    }

    public void AssignSlots()
    {
        craftingSystem.Init();

        resulterUISlot.Init(craftingSystem.resulterSlot);

        for (int i = 0; i < craftingSystem.inventorySlots.Length; i++)
        {
            slotDictionary.Add(uiSlots[i], craftingSystem.inventorySlots[i]);

            uiSlots[i].Init(craftingSystem.inventorySlots[i]);
        }

        slotDictionary.Add(resulterUISlot, craftingSystem.resulterSlot);
    }

    public void UpdateUISlot(InventorySlot slotToUpdate)
    {
        foreach (var slot in slotDictionary)
        {
            if (slotToUpdate == slot.Value)
            {
                slot.Key.UpdateUISlot();
            }
        }
    }

    public void SlotUIClicked(CraftingSlotUI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.itemData == null && mouseInventorySlotUI.AssignedInventorySlot.itemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventorySlotUI.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventorySlotUI.AssignedInventorySlot.ClearSlot();
            mouseInventorySlotUI.UpdateUISlot();
        }

        else if (clickedUISlot.AssignedInventorySlot.itemData != null && mouseInventorySlotUI.AssignedInventorySlot.itemData == null)
        {
            mouseInventorySlotUI.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);
            mouseInventorySlotUI.UpdateUISlot();
            clickedUISlot.ClearSlot();
        }

        else if (clickedUISlot.AssignedInventorySlot.itemData != null && mouseInventorySlotUI.AssignedInventorySlot.itemData != null)
        {
            if (clickedUISlot.AssignedInventorySlot.ItemData == mouseInventorySlotUI.AssignedInventorySlot.ItemData)
            {
                if (clickedUISlot.AssignedInventorySlot.GetAvailableSize(mouseInventorySlotUI.AssignedInventorySlot.StackSize, out int availableSize))
                {
                    clickedUISlot.AssignedInventorySlot.AddToStack(availableSize);
                    clickedUISlot.UpdateUISlot();

                    mouseInventorySlotUI.AssignedInventorySlot.RemoveFromStack(availableSize);
                    mouseInventorySlotUI.UpdateUISlot();

                    if (mouseInventorySlotUI.AssignedInventorySlot.IsSlotEmpty())
                    {
                        mouseInventorySlotUI.ClearSlot();
                    }

                    return;
                }
            }

            SwapSlots(clickedUISlot);
        }
    }

    void SwapSlots(CraftingSlotUI clickedUISlot)
    {
        InventorySlot clonedSlot = new InventorySlot(mouseInventorySlotUI.AssignedInventorySlot.ItemData, mouseInventorySlotUI.AssignedInventorySlot.StackSize);

        mouseInventorySlotUI.ClearSlot();

        mouseInventorySlotUI.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);
        mouseInventorySlotUI.UpdateUISlot();

        clickedUISlot.AssignedInventorySlot.UpdateSlot(clonedSlot.ItemData, clonedSlot.StackSize);
        clickedUISlot.UpdateUISlot();
    }

    public void CraftItems()
    {
        craftingSystem.CraftItems();
    }
}