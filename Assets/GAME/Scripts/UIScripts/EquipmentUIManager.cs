using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentUIManager : MonoBehaviour
{
    [SerializeField] GameObject equipmentWindow;

    private void Update()
    {
        if (Keyboard.current.f5Key.wasPressedThisFrame) equipmentWindow.SetActive(!equipmentWindow.activeInHierarchy);
        if (Keyboard.current.escapeKey.wasPressedThisFrame) equipmentWindow.SetActive(false);
    }
}
