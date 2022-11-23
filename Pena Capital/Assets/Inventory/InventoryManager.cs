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
    public Transform SelectedContent;
    public GameObject InventoryItem;
    public InventoryItemController[] InventoryItems;
    public Item selected;
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
    List<string> names()
    {
        List<string> res = new List<string>();
        foreach (var i in Items) res.Add(i.name);
        return res;
    }
    public void ListItems()
    {
        foreach (var item in Items)
        {
            // List<string> res = names();
            // if (res.Contains(InventoryItem.name))
            // {
                InventoryItem.name = item.name;
                if (ItemContent.Find(InventoryItem.name + "(Clone)") == null)
                {
                    GameObject obj = Instantiate(InventoryItem, ItemContent);
                    var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                    var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                    ItemName.text = item.itemName;
                    ItemIcon.sprite = item.icon;
                }
            // }
        }
        SetInventoryItems();
    }
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
            InventoryItems[i].AddItem(Items[i]);
    }
    public void SetSelected(Item i)
    {
        foreach (Transform t in SelectedContent)
            Destroy(t.gameObject);
        selected = i;
        GameObject obj = Instantiate(InventoryItem, SelectedContent);
        var ItemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        ItemName.text = i.itemName;
        ItemIcon.sprite = i.icon;
    }
}
