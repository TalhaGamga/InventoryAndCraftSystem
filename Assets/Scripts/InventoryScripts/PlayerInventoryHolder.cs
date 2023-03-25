using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] InventorySystem secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    private void Start()
    {
        secondaryInventorySystem.Init();
    }

    public override bool AddToInventory(InventoryItemData itemData)
    {
        if (primaryInventorySystem.AddToInventory(itemData,1)) return true;

        if (secondaryInventorySystem.AddToInventory(itemData, 1)) return true;

        return false;
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
    }
}
