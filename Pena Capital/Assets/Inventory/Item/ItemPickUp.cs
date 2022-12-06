using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        SavingElements.Instance.pickedItems.Add(this.gameObject.name);
        Destroy(gameObject);
        DisplayText.Instance.changeText("");
    }
    void OnMouseDown(){
        PickUp();
    }
    void OnMouseEnter(){
        DisplayText.Instance.changeText("Coger " + Item.name);
    }
    void OnMouseExit(){
        DisplayText.Instance.changeText("");
    }
}
