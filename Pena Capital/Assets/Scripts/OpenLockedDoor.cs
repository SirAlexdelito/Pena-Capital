using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenLockedDoor : MonoBehaviour
{
    public bool doorOpened;
    private bool coroutineAllowed;
    private float a;
    private float b;
    private float c;
    private InventoryManager IM;
    private GameObject Inventory;
    public float g;
    public string comprobar;
    //-90,0,-75
    public Quaternion startingPos{ get;private set;}
    public Quaternion actualPos{get;private set;}
    //public GameObject doorWing; 
    private bool inRange;
    private RaycastHit aux;
    private bool golpeado;
    void Awake()
    {
        doorOpened = false;
        coroutineAllowed = true;
        a = transform.localRotation.eulerAngles.x;
        b = transform.localRotation.eulerAngles.y;
        c = transform.localRotation.eulerAngles.z;
        startingPos = Quaternion.Euler(a,b,c);
        actualPos = startingPos;
        IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }
    public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        RaycastHit hit;
        if(Physics.Raycast(characterController.transform.position, characterController.transform.forward, hitInfo: out hit,
                               5, LayerMask.GetMask("EnRango"))){
                                aux=hit;
                                golpeado=true;
                                hit.transform.gameObject.GetComponent<OpenLockedDoor>().inRange=true;
                               }
        else if(golpeado){
            golpeado=false;
            aux.transform.gameObject.GetComponent<OpenLockedDoor>().inRange = false;
        }
    }
    public bool IsOpen(){
        if(actualPos!=startingPos)
        return true; else return false;
    }
    public void OpenStatic(){
        doorOpened=true;
        transform.localRotation = Quaternion.Euler(a, g, c);
    }
    private void OnMouseDown()
    {
        if (inRange){
        Invoke("RunCoroutine", 0f);
        }
    }

    private void RunCoroutine()
    {
        if (coroutineAllowed)
        { StartCoroutine("OpenThatDoor"); }
    }

    private IEnumerator OpenThatDoor()
    {
        if (!Inventory.activeSelf)
        {
            if (!doorOpened)
            {
                if (IM.selected != null)
                {
                    if (IM.selected.itemName == comprobar)
                    {
                        coroutineAllowed = false;
                        if (g >= 0)
                        {
                            if (!doorOpened)
                            {
                                for (float i = b; i <= (b + g); i += 3f)
                                {
                                    transform.localRotation = Quaternion.Euler(a, +i, c);
                                    actualPos = Quaternion.Euler(a, +i, c);
                                    GameObject.FindGameObjectWithTag("DoorSound").GetComponent<AudioSource>().Play();
                                    yield return new WaitForSeconds(0f);
                                }
                                SavingElements.Instance.lockedDoors.Add(this);
                                doorOpened = true;
                            }
                            else
                            {
                                for (float i = (b + g); i >= b; i -= 3f)
                                {
                                    transform.localRotation = Quaternion.Euler(a, +i, c);
                                    actualPos = Quaternion.Euler(a, +i, c);
                                    yield return new WaitForSeconds(0f);
                                }
                                doorOpened = false;
                            }
                        }
                        else
                        {
                            if (!doorOpened)
                            {
                                for (float i = b; i >= (b + g); i -= 3f)
                                {
                                    transform.localRotation = Quaternion.Euler(a, +i, c);
                                    actualPos = Quaternion.Euler(a, +i, c);
                                    GameObject.FindGameObjectWithTag("DoorSound").GetComponent<AudioSource>().Play();
                                    yield return new WaitForSeconds(0f);
                                }
                                SavingElements.Instance.lockedDoors.Add(this);
                                
                                doorOpened = true;
                            }
                            else
                            {
                                for (float i = (b + g); i <= b; i += 3f)
                                {
                                    transform.localRotation = Quaternion.Euler(a, +i, c);
                                    actualPos = Quaternion.Euler(a, +i, c);
                                    yield return new WaitForSeconds(0f);
                                }
                                doorOpened = false;
                            }
                        }
                        coroutineAllowed = true;
                        var itemNameFull = IM.SelectedContent.GetChild(0).name;
                        var itemName = itemNameFull.Substring(0,itemNameFull.Length-7);
                        var objectContent = IM.ItemContent.Find(itemName);
                        var objectSelected = IM.SelectedContent.GetChild(0);
                        GameObject.DestroyImmediate(objectContent.gameObject);
                        GameObject.DestroyImmediate(objectSelected.gameObject);
                        // IM.InventoryItem = IM.ItemContent.GetChild(0).gameObject;
                        IM.Remove(IM.selected);
                        // IM.selected = null;
                        DisplayText.Instance.changeText("usado " + comprobar);
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                    }
                    else
                    {
                        DisplayText.Instance.changeText("con esto no puedo abrir esta puerta");
                        yield return new WaitForSeconds(2);
                        DisplayText.Instance.changeText("");
                    }
                }
                else
                {
                    DisplayText.Instance.changeText("necesito algo para abrir esta puerta");
                    yield return new WaitForSeconds(2);
                    DisplayText.Instance.changeText("");
                }
            }
        }
    }
    void OnMouseOver()
    {
        if (inRange){
            if (!doorOpened){
                if (!Inventory.activeSelf){
                    DisplayText.Instance.changeText("Abrir puerta");
                }
                else{
                    DisplayText.Instance.changeText("");
                }
            }
            else{
                DisplayText.Instance.changeText("");
            }
        }
        else{
            DisplayText.Instance.changeText("");
        }
    }
    void OnMouseExit()
    {
        DisplayText.Instance.changeText("");
    }
}