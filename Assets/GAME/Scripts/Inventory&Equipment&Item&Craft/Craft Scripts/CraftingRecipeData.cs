using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/CraftingRecipeData")]
public class CraftingRecipeData : ScriptableObject
{
    [SerializeField] InputObject[] inputObjects;

    [SerializeField] InventoryItemData craftedObject;
}

[System.Serializable]
public class InputObject
{
    public InventoryItemData inputItem;
    public int size;
}
