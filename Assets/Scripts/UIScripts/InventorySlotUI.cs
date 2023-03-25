using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;

    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    public InventoryDisplayer inventoryDisplayer { get; private set; }

    private void Awake()
    {
        ClearSlot();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSlotUI);

        inventoryDisplayer = GetComponentInParent<InventoryDisplayer>();
    }

    public void Init(InventorySlot slotToAssign)
    {
        assignedInventorySlot = slotToAssign;
    }

    private void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData!=null)
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


    private void OnClickSlotUI()
    {
        inventoryDisplayer?.SlotUIClicked(this);
        Debug.Log("OnClickUI");
    }
}
