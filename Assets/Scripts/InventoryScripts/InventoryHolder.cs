using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;

    private void Awake()
    {
        inventorySystem.Init();
    }

    public InventorySystem InventorySystem => inventorySystem;

    public bool AddToInventory(InventoryItemData itemData)
    {
        return InventorySystem.AddToInventory(itemData, 1);
    }
}