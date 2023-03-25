using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class InventoryHolder : MonoBehaviour
{
    public InventorySystem primaryInventorySystem;

    public static Action<InventorySystem> OnInventoryDisplayRequested;
    private void Awake()
    {
        primaryInventorySystem.Init(); 
    }

    public InventorySystem InventorySystem => primaryInventorySystem;

    public abstract bool AddToInventory(InventoryItemData itemData);
}