using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDisplayer : MonoBehaviour
{
    [SerializeField] private EquipmentSystem equipmentSystem;

    [SerializeField] private EquipmentSlotUI[] uiSlots;

    [SerializeField] private MouseInventorySlotUI mouseInventorySlotUI;

    public Dictionary<EquipmentSlotUI, EquipmentSlot> slotDictionary;

    private void Start()
    {
        AssignSlots();

        equipmentSystem.OnEquipmentSlotChanged += UpdateUISlot;
    }

    public void AssignSlots()
    {
        equipmentSystem.Init();

        slotDictionary = new Dictionary<EquipmentSlotUI, EquipmentSlot>();

        for (int i = 0; i < equipmentSystem.size; i++)
        {
            slotDictionary.Add(uiSlots[i], equipmentSystem.equipmentSlots[i]);

            uiSlots[i].Init(equipmentSystem.equipmentSlots[i]);
        }
    }

    public void SlotUIClicked(EquipmentSlotUI equipmentSlotUI)
    {
        if (mouseInventorySlotUI.AssignedInventorySlot.ItemData != null && mouseInventorySlotUI.AssignedInventorySlot.ItemData.itemType != ItemType.equipment)
        {
            return;
        }

        if (equipmentSlotUI.AssignedEquipmentSlot.itemData == null && mouseInventorySlotUI.AssignedInventorySlot.itemData != null)
        {
            if (equipmentSlotUI.CheckBodyType((EquipmentItemData)mouseInventorySlotUI.AssignedInventorySlot.itemData))
            {
                equipmentSlotUI.AssignedEquipmentSlot.AssignItem((EquipmentItemData)mouseInventorySlotUI.AssignedInventorySlot.itemData);
                equipmentSlotUI.UpdateUISlot();
                mouseInventorySlotUI.ClearSlot();
            }
        }

        else if (equipmentSlotUI.AssignedEquipmentSlot.itemData != null && mouseInventorySlotUI.AssignedInventorySlot.itemData == null)
        {
            EquipmentSlot equipmentSlot = new EquipmentSlot(equipmentSlotUI.AssignedEquipmentSlot.itemData);

            equipmentSlotUI.AssignedEquipmentSlot.UnEquipItem();//Unequip and clear
            equipmentSlotUI.UpdateUISlot();
             
            mouseInventorySlotUI.AssignedInventorySlot.UpdateSlot(equipmentSlot.itemData, 1);
            mouseInventorySlotUI.UpdateUISlot();
        }

        else if (equipmentSlotUI.AssignedEquipmentSlot.itemData != null && mouseInventorySlotUI.AssignedInventorySlot.itemData != null)
        {
            EquipmentItemData mouseEquipmentData = (EquipmentItemData)mouseInventorySlotUI.AssignedInventorySlot.itemData;

            if (mouseEquipmentData.bodyPart == equipmentSlotUI.AssignedEquipmentSlot.itemData.bodyPart)
            {
                mouseInventorySlotUI.AssignedInventorySlot.UpdateSlot(equipmentSlotUI.AssignedEquipmentSlot.itemData, 1);
                mouseInventorySlotUI.UpdateUISlot();

                equipmentSlotUI.AssignedEquipmentSlot.UpdateSlot(mouseEquipmentData);
                equipmentSlotUI.UpdateUISlot();
            }
        }
    }

    private void UpdateUISlot(EquipmentSlot slotToUpdate)
    {
        foreach (var slot in slotDictionary)
        {
            if (slotToUpdate == slot.Value)
            {
                slot.Key.UpdateUISlot();
            }
        }
    }
}