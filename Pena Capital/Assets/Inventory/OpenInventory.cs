
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject Inventory;
    public static OpenInventory Instance;
    public InventoryManager InventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        Instance=this;
        Inventory.SetActive(false);
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("OpenInventory"))
        {
            
            if (Inventory.activeSelf) { 
                Inventory.SetActive(false);
            }
            else {
                InventoryManager.ListItems();
                Inventory.SetActive(true);
            }
        }
        
    }
    public void Close()
    {
        Inventory.SetActive(false);
    }
}
