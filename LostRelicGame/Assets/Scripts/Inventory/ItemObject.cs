using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    private bool newInventory = true;
    public GameObject InventoryBar;

    public void PickupItem()
    {
        if (newInventory)
        {
            //this is to make the inventory bar appear; its transform is orginally (0, 0, 0)
            InventoryBar.transform.localScale = new Vector3(0.25f, 0.25f, 1);
            newInventory = false;
        }
        InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }
}
