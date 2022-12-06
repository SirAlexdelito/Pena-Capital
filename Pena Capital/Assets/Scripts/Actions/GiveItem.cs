using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : Action
{
    override
    public IEnumerator Act()
    {
        InventoryManager.Instance.Add(Item);
        SavingElements.Instance.pickedItems.Add(this.gameObject.name);
        DisplayText.Instance.changeText("Encontrado " + Item.itemName);
        yield return new WaitForSeconds(2);
        DisplayText.Instance.changeText("");
    }

}
