using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Auxpuerta : MonoBehaviour
{
    public bool interacted;
    private bool coroutineAllowed;
    private InventoryManager IM;
    private GameObject Inventory;
    public string comprobar;
    public string actionName;
    public Action action;
    private bool inRange;
    private bool espera;
    public int rango;
    private bool golpeado;
    void Start()
    {
        interacted = false;
        coroutineAllowed = true;
        inRange=false;
        espera=false;
        IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }
    public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        RaycastHit hit;
        if(Physics.Raycast(characterController.transform.position, characterController.transform.forward, hitInfo: out hit,
                               rango, LayerMask.GetMask("EnRango"))){

                                golpeado=true;
                                inRange=true;
                               }
        else if(golpeado){
            golpeado = false;
            inRange = false;
        }
    }
    /*public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        if(Vector3.Distance(transform.position, characterController.transform.position) <= rango)
        {inRange=true;}
        else{inRange=false;}
    }*/
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
                if (IM.selected != null)
                {
                    if (IM.selected.itemName == comprobar)
                    {
                        coroutineAllowed = false;
                        
                        interacted=true;

                        var itemNameFull = IM.SelectedContent.GetChild(0).name;
                        var itemName = itemNameFull.Substring(0,itemNameFull.Length-7);
                        var objectContent = IM.ItemContent.Find(itemName);
                        var objectSelected = IM.SelectedContent.GetChild(0);
                        GameObject.DestroyImmediate(objectContent.gameObject);
                        GameObject.DestroyImmediate(objectSelected.gameObject);
                        // IM.InventoryItem = IM.ItemContent.GetChild(0).gameObject;
                        IM.Remove(IM.selected);
                        action.RunCoroutine();
                        espera = true;
                        DisplayText.Instance.changeText("usado " + comprobar);
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                        espera = false;
                        coroutineAllowed = true;
                    }
                    else
                    {
                        espera=true;
                        DisplayText.Instance.changeText("esto no me sirve");
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                        espera=false;
                    }
                }
                else
                {
                    espera=true;
                    DisplayText.Instance.changeText("tengo que usar algún objeto");
                    yield return new WaitForSeconds(2);
                    DisplayText.Instance.changeText("");
                    espera=false;
                }
            }
        }
    }
    void OnMouseOver()
    {
        if(!espera){
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
    }
    void OnMouseExit()
    {
        if (!interacted)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText("");
    }
}