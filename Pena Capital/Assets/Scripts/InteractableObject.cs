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

    //public GameObject doorWing; 

    // Start is called before the first frame update
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

                        //DO

                        coroutineAllowed = true;
                        GameObject.Destroy(IM.ItemContent.Find((IM.SelectedContent.GetChild(0)).name).gameObject);
                        IM.Items.Remove(IM.selected);
                        GameObject.Destroy(IM.SelectedContent.GetChild(0).gameObject);
                        IM.selected = null;
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