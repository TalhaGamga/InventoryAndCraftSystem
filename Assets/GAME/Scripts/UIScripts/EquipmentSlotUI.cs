using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;

    [SerializeField] private EquipmentSlot assignedEquipmentSlot;

    private Button button;

    public EquipmentSlot AssignedEquipmentSlot => assignedEquipmentSlot;

    public EquipmentDisplayer equipmentDisplayer { get; private set; }

    public Transform attachTransform;

    public BodyPart bodyPart;

    private void Awake()
    {   
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSlotUI);

        equipmentDisplayer = GetComponentInParent<EquipmentDisplayer>();
    }

    private void UpdateUISlot(EquipmentSlot slot)
    {
        if (slot.itemData != null) 
        {
            itemSprite.sprite = slot.itemData.Icon;
            itemSprite.color = Color.white;
        }

        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (AssignedEquipmentSlot != null) UpdateUISlot(AssignedEquipmentSlot);
    }

    public void Init(EquipmentSlot slotToAssign)
    {
        assignedEquipmentSlot = slotToAssign;

        assignedEquipmentSlot.attachTransform = attachTransform;

        assignedEquipmentSlot.AssignBodyPart(bodyPart);
    }

    private void ClearSlot()
    {
        AssignedEquipmentSlot?.ClearSlot();
        itemSprite.sprite = null;

        itemSprite.color = Color.clear;
    }

    public void OnClickSlotUI()
    {
        equipmentDisplayer?.SlotUIClicked(this);
    }

    public bool CheckBodyType(EquipmentItemData itemData)
    {
        if (itemData.bodyPart == bodyPart)
        {
            return true;
        }

        return false;
    }
}
