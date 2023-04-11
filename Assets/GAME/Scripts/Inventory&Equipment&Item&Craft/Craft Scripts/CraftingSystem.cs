using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CraftingSystem")]
public class CraftingSystem : ScriptableObject
{
    public CraftingRecipeData[] craftingRecipeDatas;
    public InventorySlot[] inventorySlots;
    public InventorySlot resulterSlot;

    Dictionary<HashSet<InputItem>, InventoryItemData> craftDict;
    public Action<InventorySlot> OnCraftingSlotUpdated;

    public void Init()
    {
        craftDict = new Dictionary<HashSet<InputItem>, InventoryItemData>();
        resulterSlot = new InventorySlot();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = new InventorySlot();
        }

        for (int i = 0; i < craftingRecipeDatas.Length; i++)
        {
            craftingRecipeDatas[i].InitData();
            craftDict.Add(craftingRecipeDatas[i].inputItemSet, craftingRecipeDatas[i].resultData);
        }
    }

    public bool CraftItems()
    {
        int size = inventorySlots.Length;

        HashSet<InputItem> inputItemSet = new HashSet<InputItem>();

        for (int i = 0; i < size; i++)
        {
            InventorySlot inventorySlot = inventorySlots[i];
            InventoryItemData itemData = inventorySlot.itemData;

            if (itemData != null)
            { 
                InputItem inputItem = new InputItem(itemData, inventorySlot.StackSize);
                inputItemSet.Add(inputItem);
            }
        }

        foreach (HashSet<InputItem> craftDictKey in craftDict.Keys)
        {
            bool isEqual = inputItemSet.SetEquals(craftDictKey);
            if (isEqual)
            {
                InventoryItemData resultItemData = craftDict[craftDictKey];
                resulterSlot.AssignItem(new InventorySlot(resultItemData, 1));

                OnCraftingSlotUpdated?.Invoke(resulterSlot);

                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlot tempSlot = inventorySlots[i];

                    if (tempSlot.itemData!=null)
                    {
                        tempSlot.ClearSlot(); 
                        OnCraftingSlotUpdated?.Invoke(tempSlot);
                    }
                }

                return true;
            }
        }

        return false;
    }
}