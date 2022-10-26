using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    private void Awake()
    {
        Instance = this;
    }
    public void Add(Item item) 
    {
        Items.Add(item);
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    public void ListItems()
    {
        //clean list before opening to avoid duplicates
        foreach (Transform item in ItemContent)
            Destroy(item.gameObject);
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            ItemName.text = item.itemName;
            ItemIcon.sprite = item.icon;
        }
    }
}
