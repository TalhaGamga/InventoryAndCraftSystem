using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemHolder : MonoBehaviour
{
    [SerializeField] CraftingSystem craftingSystem;

    private void Awake()
    {
        craftingSystem.Init();
    }

    public bool CraftItems()
    {
        if (craftingSystem.CraftItems()) return true;

        return false;
    }
}
