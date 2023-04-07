using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] InventoryItemData itemData;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out InventoryHolder inventoryHolder))
        {
            if (itemData.Collect(inventoryHolder))
            {
                Destroy(gameObject);
            }
        }
    }
}
