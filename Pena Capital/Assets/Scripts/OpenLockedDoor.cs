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
    public float g;
    public string comprobar;

    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        a=transform.localRotation.eulerAngles.x;
        b=transform.localRotation.eulerAngles.y;
        c=transform.localRotation.eulerAngles.z;
        IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    private void OnMouseDown()
    {
        Invoke("RunCoroutine", 0f);
    }

    private void RunCoroutine()
    {
        if (coroutineAllowed)
            {StartCoroutine("OpenThatDoor");}
    }