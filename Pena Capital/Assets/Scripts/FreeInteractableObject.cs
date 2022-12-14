using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FreeInteractableObject : MonoBehaviour
{
    public bool interacted;
    private bool coroutineAllowed;
    private InventoryManager IM;
    private GameObject Inventory;
    public string actionName;
    public Action action;
    public string comprobar;
    private bool inRange;

    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        interacted = false;
        coroutineAllowed = true;
        inRange = false;
        IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }
    public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        if(Vector3.Distance(transform.position, characterController.transform.position) <= 2)
        {inRange=true;}
        else{inRange=false;}
    }
    private void OnMouseDown()
    {
        if(inRange){
        Invoke("RunCoroutine", 0f);
        }
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
                if (comprobar == "")
                {
                    coroutineAllowed = false;
                    action.RunCoroutine();
                    interacted=true;
                    coroutineAllowed = true;
                    //DisplayText.Instance.changeText("Usado");
                    //yield return new WaitForSeconds(2);
                    //DisplayText.Instance.changeText("");
                }
                else if (IM.selected != null)
                {
                    if (IM.selected.itemName == comprobar)
                    {
                    coroutineAllowed = false;
                    action.RunCoroutine();
                    interacted=true;
                    coroutineAllowed = true;
                    // GameObject.Destroy(IM.ItemContent.Find((IM.SelectedContent.GetChild(0)).name).gameObject);
                    // IM.Items.Remove(IM.selected);
                    // GameObject.Destroy(IM.SelectedContent.GetChild(0).gameObject);
                    // IM.selected = null;
                    //DisplayText.Instance.changeText("Usado " + comprobar);
                    //yield return new WaitForSeconds(2);
                    //DisplayText.Instance.changeText("");
                    }
                    else
                    {
                        DisplayText.Instance.changeText("Necesitas " + comprobar);
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");  
                    }
                }
                else
                {
                    DisplayText.Instance.changeText("Necesitas " + comprobar);
                    yield return new WaitForSeconds(2);
                    DisplayText.Instance.changeText("");
                }
            }
        }
    }
    void OnMouseOver()
    {
        if(inRange)
        {
            if (!interacted){
                if (!Inventory.activeSelf){
                    DisplayText.Instance.changeText(actionName);
                }
                else {DisplayText.Instance.changeText("");}
            }
        }
        else {DisplayText.Instance.changeText("");}
    }
    void OnMouseExit()
    {
        if (!interacted)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText("");
    }
}