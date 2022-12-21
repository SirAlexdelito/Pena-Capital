using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class ItemPickUp : MonoBehaviour
{
    public Item Item;
    private bool inRange = false;
    public string texto; 
    void PickUp()
    {
        inRange=false;
        InventoryManager.Instance.Add(Item);
        SavingElements.Instance.pickedItems.Add(this.gameObject.name);
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("PickupSound").GetComponent<AudioSource>().Play();
        DisplayText.Instance.changeText(texto);
    }
    /*public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        RaycastHit hit;
        if(Physics.Raycast(characterController.transform.position, characterController.transform.forward, hitInfo: out hit,
                               10, LayerMask.GetMask("EnRango"))){
                                golpeado=true;
                                inRange=true;
                               }
        else if(golpeado){
            golpeado = false;
            inRange = false;
        }
    }*/
    public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        if(Vector3.Distance(transform.position, characterController.transform.position) <= 3)
        {inRange=true;}
        else{inRange=false;}
    }
    void OnMouseDown(){
        if(inRange){
        PickUp();
        }
    }
    void OnMouseOver(){
        if(inRange){
        DisplayText.Instance.changeText("Coger " + Item.name);
        }
        else{DisplayText.Instance.changeText("");}
    }
    void OnMouseExit(){
        DisplayText.Instance.changeText("");
    }
}
