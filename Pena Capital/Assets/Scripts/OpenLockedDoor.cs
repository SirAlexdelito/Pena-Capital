using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockedDoor : MonoBehaviour
{
    private bool doorOpened;
    private bool coroutineAllowed;
    private float a;
    private float b;
    private float c;
    private InventoryManager IM;
    private GameObject Inventory;
    public float g;
    public string comprobar;

    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        a = transform.localRotation.eulerAngles.x;
        b = transform.localRotation.eulerAngles.y;
        c = transform.localRotation.eulerAngles.z;
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
        { StartCoroutine("OpenThatDoor"); }
    }

    private IEnumerator OpenThatDoor()
    {
        if (!Inventory.activeSelf)
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
                                yield return new WaitForSeconds(0f);
                            }
                            doorOpened = true;
                        }
                        else
                        {
                            for (float i = (b + g); i >= b; i -= 3f)
                            {
                                transform.localRotation = Quaternion.Euler(a, +i, c);
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
                                yield return new WaitForSeconds(0f);
                            }
                            doorOpened = true;
                        }
                        else
                        {
                            for (float i = (b + g); i <= b; i += 3f)
                            {
                                transform.localRotation = Quaternion.Euler(a, +i, c);
                                yield return new WaitForSeconds(0f);
                            }
                            doorOpened = false;
                        }
                    }
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
    void OnMouseEnter()
    {
        if (!Inventory.activeSelf)
            DisplayText.Instance.changeText("Abrir puerta");
    }
    void OnMouseExit()
    {
        if (!Inventory.activeSelf)
            DisplayText.Instance.changeText("");
    }
}