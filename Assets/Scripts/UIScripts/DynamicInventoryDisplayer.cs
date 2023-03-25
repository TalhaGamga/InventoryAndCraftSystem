using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DynamicInventoryDisplayer : InventoryDisplayer
{
    [SerializeField] private InventorySlotUI slotPrefab;

    public void RefreshDynamicInventory(InventorySystem invToDisplay) 
    {
        inventorySystem = invToDisplay;
        ClearSlots();
        inventorySystem.OnInventorySlotChanged += UpdateUISlot;
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();
        AssignSlots(inventorySystem);
    }

    public override void AssignSlots(InventorySystem invToDisplay)
    {
        int size = invToDisplay.InventorySize;

        for (int i = 0; i < size; i++)
        {
            InventorySlotUI invSlotUI = Instantiate(slotPrefab, transform);

            InventorySlot invSlot = invToDisplay.InventorySlots[i];

            invSlotUI.Init(invSlot);

            invSlotUI.UpdateUISlot();

            slotDictionary.Add(invSlotUI, invSlot);
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
}
