using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems; 
 
public class MouseInventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;

    [SerializeField] private InventorySlot assignedInventorySlot;

    [SerializeField] public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    private void Awake()
    {
        itemSprite.color = Color.clear; 
        itemCount.text = "";
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsMousePointingUIElements())
            {
                ClearSlot();
            }
        }
    }

    private void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }

        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (AssignedInventorySlot != null) UpdateUISlot(AssignedInventorySlot);
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();

        itemSprite.sprite = null;

        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    private bool IsMousePointingUIElements()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
