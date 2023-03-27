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

    internal void SlotUIClicked(EquipmentSlotUI equipmentSlotUI) //FULLFILL HEREEE!
    {
        if (mouseInventorySlotUI.AssignedInventorySlot.ItemData == null || mouseInventorySlotUI.AssignedInventorySlot.ItemData.itemType != ItemType.equipment)
        {
            return;
        }

        if (mouseInventorySlotUI.AssignedInventorySlot.ItemData.GetType() == typeof(EquipmentItemData))
        {
            if (equipmentSystem.EquipItem((EquipmentItemData)mouseInventorySlotUI.AssignedInventorySlot.itemData, equipmentSlotUI.AssignedEquipmentSlot))
            {
                mouseInventorySlotUI.ClearSlot();

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