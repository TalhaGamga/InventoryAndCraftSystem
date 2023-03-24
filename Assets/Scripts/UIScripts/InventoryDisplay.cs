using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseInventorySlotUI mouseInventorySlotUI;

    [SerializeField] private InventorySystem inventorySystem;
    public InventorySystem InventorySystem => inventorySystem;

    private Dictionary<InventorySlotUI, InventorySlot> slotDictionary;
    public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary => slotDictionary;

    [SerializeField] private InventorySlotUI slotPrefab;

    private void Start()
    {
        ClearSlots();

        AssignSlots();

        InventorySystem.OnInventorySlotChanged += UpdateUISlot;
    }

    private void AssignSlots()
    {
        int size = inventorySystem.InventorySize;

        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            InventorySlotUI uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.InventorySlots[i]);

            uiSlot.Init(InventorySystem.InventorySlots[i]);

            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach (Transform item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }

    private void UpdateUISlot(InventorySlot slotToUpdate)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slotToUpdate == slot.Value)
            {
                slot.Key.UpdateUISlot();
            }
        }
    }

    /// <summary>
    /// Clicked slot has an item - mouse doesn't have an item - pick up that item +
    /// If player is holding shift key, split the stack. +
    /// Pick up the item in the clicked slot +
    /// Clicked slot doesn't have an item - Mouse have an item - place the item into the empty slot.+
    /// Is the slot stack size + mouse stack size > slot Max stack size? If so, take from mouse +
    /// 
    /// Both slots have an item - decide what to do:
    /// Are both item the same? If so combine them. + 
    /// Stack is full, so swap the items.+
    /// Slot is not at max, so take what's need from the mouse inventory.+
    /// </summary>

    public void SlotUIClicked(InventorySlotUI clickedUISlot) //Fix HERE ! 
    {
        bool isShiftKeyPressed = Keyboard.current.leftShiftKey.isPressed;

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventorySlotUI.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventorySlotUI.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();
            mouseInventorySlotUI.ClearSlot();
        }

        else if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventorySlotUI.AssignedInventorySlot.ItemData == null)
        {
            if (!isShiftKeyPressed)
            {
                mouseInventorySlotUI.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);

                mouseInventorySlotUI.UpdateUISlot();

                clickedUISlot.ClearSlot();
            }

            else
            {
                if (clickedUISlot.AssignedInventorySlot.SplitSlotStack(out InventorySlot splitSlot))
                {
                    clickedUISlot.UpdateUISlot();

                    mouseInventorySlotUI.AssignedInventorySlot.AssignItem(splitSlot);
                    mouseInventorySlotUI.UpdateUISlot();
                }
            }
        }

        else if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventorySlotUI.AssignedInventorySlot.ItemData != null)
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
                        mouseInventorySlotUI.UpdateUISlot();
                    }

                    return;
                }
            }

            SwapSlots(clickedUISlot);
        }
    }

    void SwapSlots(InventorySlotUI clickedUISlot) 
    {
        InventorySlot clonedSlot = new InventorySlot(mouseInventorySlotUI.AssignedInventorySlot.ItemData, mouseInventorySlotUI.AssignedInventorySlot.StackSize);

        mouseInventorySlotUI.ClearSlot();

        mouseInventorySlotUI.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);
        mouseInventorySlotUI.UpdateUISlot();

        clickedUISlot.AssignedInventorySlot.UpdateSlot(clonedSlot.ItemData, clonedSlot.StackSize);
        clickedUISlot.UpdateUISlot();
    }
}
