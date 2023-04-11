using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;

    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    public CraftingDisplayer craftingDisplayer { get; private set; }

    Color initColor;

    private void Awake()
    {
        initColor = itemSprite.color;

        ClearSlot();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSlotUI);

        craftingDisplayer = GetComponentInParent<CraftingDisplayer>();
    }

    public void Init(InventorySlot slotToAssign)
    {
        assignedInventorySlot = slotToAssign;
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

        itemSprite.color = initColor;
        itemCount.text = "";
    }


    private void OnClickSlotUI()
    {
        craftingDisplayer?.SlotUIClicked(this);
    }
}
