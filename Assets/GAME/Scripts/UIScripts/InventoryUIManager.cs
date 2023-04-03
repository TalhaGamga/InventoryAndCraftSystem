using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] DynamicInventoryDisplayer playerBackPack;
    [SerializeField] DynamicInventoryDisplayer dynamicInventory;
    private void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
        InventoryHolder.OnInventoryDisplayRequested += DisplayDynamicInventory;
    }

    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
        InventoryHolder.OnInventoryDisplayRequested -= DisplayDynamicInventory;
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && playerBackPack.gameObject.activeInHierarchy) playerBackPack.gameObject.SetActive(false);

        if (Keyboard.current.escapeKey.wasPressedThisFrame && dynamicInventory.gameObject.activeInHierarchy) dynamicInventory.gameObject.SetActive(false);
    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackPack.gameObject.SetActive(true);
        playerBackPack.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayDynamicInventory(InventorySystem invToDisplay)
    {
        dynamicInventory.gameObject.SetActive(true);
        dynamicInventory.RefreshDynamicInventory(invToDisplay);
    }
}
