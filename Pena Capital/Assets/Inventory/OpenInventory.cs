
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject Inventory;
    public InventoryManager InventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        
        Inventory.SetActive(false);
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        InventoryManager.ListItems();
        if (Input.GetButtonDown("OpenInventory"))
        {
            if (Inventory.active) { 
                Inventory.SetActive(false);
            }
            else {
                Inventory.SetActive(true);
            }
        }
        
    }
}
