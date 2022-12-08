using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool interacted;
    private bool coroutineAllowed;
    private InventoryManager IM;
    private GameObject Inventory;
    public string comprobar;
    public string actionName;
    public Action action;
    void Start()
    {
        interacted = false;
        coroutineAllowed = true;
        IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }

    private void OnMouseDown()
    {
        Invoke("RunCoroutine", 0f);
    }

    private void RunCoroutine()
    {
        if (coroutineAllowed)
        { StartCoroutine("Interact"); }
    }

    private IEnumerator Interact()
    {
        if (!Inventory.activeSelf)
        {
            if (!interacted)
            {
                if (IM.selected != null)
                {
                    if (IM.selected.itemName == comprobar)
                    {
                        coroutineAllowed = false;

                        coroutineAllowed = false;
                        
                        interacted=true;
                        coroutineAllowed = true;

                        coroutineAllowed = true;
                        var itemNameFull = IM.SelectedContent.GetChild(0).name;
                        var itemName = itemNameFull.Substring(0,itemNameFull.Length-7);
                        var objectContent = IM.ItemContent.Find(itemName);
                        var objectSelected = IM.SelectedContent.GetChild(0);
                        GameObject.DestroyImmediate(objectContent.gameObject);
                        GameObject.DestroyImmediate(objectSelected.gameObject);
                        // IM.InventoryItem = IM.ItemContent.GetChild(0).gameObject;
                        IM.Remove(IM.selected);
                        action.RunCoroutine();
                        DisplayText.Instance.changeText("usado " + comprobar);
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                    }
                    else
                    {
                        DisplayText.Instance.changeText("esto no me sirve");
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                    }
                }
                else
                {
                    DisplayText.Instance.changeText("tengo que usar algún objeto");
                    yield return new WaitForSeconds(2);
                    DisplayText.Instance.changeText("");
                }
            }
        }
    }
    void OnMouseEnter()
    {
        if (!interacted)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText(actionName);
    }
    void OnMouseExit()
    {
        if (!interacted)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText("");
    }
}