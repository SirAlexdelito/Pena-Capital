using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InventoryItemController : MonoBehaviour
{
    public InventoryManager inventory;
    public Item Item;
    public void AddItem(Item nItem)
    {
        Item = nItem;
    }
    public void Use()
    {
        inventory.SetSelected(Item);
        
    }
}
