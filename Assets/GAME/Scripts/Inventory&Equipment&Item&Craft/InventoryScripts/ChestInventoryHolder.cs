using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestInventoryHolder : InventoryHolder, IInteractable
{
    public override bool AddToInventory(InventoryItemData itemData)
    {
        if (primaryInventorySystem.AddToInventory(itemData, 1)) return true;

        return false;
    } 

    public void Interact() 
    {
        OnInventoryDisplayRequested?.Invoke(primaryInventorySystem);
    }
}

