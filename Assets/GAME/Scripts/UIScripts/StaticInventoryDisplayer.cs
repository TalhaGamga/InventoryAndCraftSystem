using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplayer : InventoryDisplayer
{
    [SerializeField] private InventorySlotUI[] uiSlots;

    private void Start()
    {
        AssignSlots(inventorySystem);

        inventorySystem.OnInventorySlotChanged += UpdateUISlot;
    }

    public override void AssignSlots(InventorySystem invToDisplay)
    {        
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        int size = inventorySystem.InventorySize;

        if(size != uiSlots.Length) Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < size; i++)
        {
            slotDictionary.Add(uiSlots[i], inventorySystem.inventorySlots[i]);

            uiSlots[i].Init(inventorySystem.inventorySlots[i]);   
        }
    }
}
