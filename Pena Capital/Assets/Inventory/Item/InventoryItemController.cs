using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InventoryItemController : MonoBehaviour
{
    public GameObject Inventory;
    public Item Item;
    public void AddItem(Item nItem)
    {
        Item = nItem;
    }
    public void Use()
    {
        if(this.transform.parent.name=="SelectedItemContent")return;
        InventoryManager.Instance.InventoryItem=this.gameObject;
        InventoryManager.Instance.SetSelected(Item);
        CursorVisibility.Instance.Close();
        OpenInventory.Instance.Close();
    }
}
