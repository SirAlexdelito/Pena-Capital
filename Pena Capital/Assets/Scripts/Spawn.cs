using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CI.QuickSave;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void load()
    {
        var screen = GameObject.FindGameObjectWithTag("Screen");
        var character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        var characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        var IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        var qRead = QuickSaveReader.Create("DATA");
        // string s = qRead.Read<string>("Scene");
        // SceneManager.LoadScene(s);
        var pos = qRead.Read<Vector3>("Position");
        var levelItems = new List<string>();
        var InventoryItems = new List<Item>();
        qRead.GetAllKeys().ToList().ForEach(x =>
        {
            if (x.Contains("Ilevel"))
                levelItems.Add(qRead.Read<string>(x));
            else if (x.Contains("Iinventory"))
                InventoryItems.Add(qRead.Read<Item>(x));
        });
        foreach (var o in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (o.CompareTag("PickeableObject"))
                if (!levelItems.Contains(o.name))
                    GameObject.Destroy(o);
            if (o.CompareTag("Door"))
                if (o.GetComponent<Transform>().rotation != qRead.Read<Quaternion>("Door: " + o.name))
                {
                    o.GetComponent<Transform>().rotation = qRead.Read<Quaternion>("Door: " + o.name);
                    o.GetComponent<OpenDoor>().doorOpened = !o.GetComponent<OpenDoor>().doorOpened;
                }
            if (o.CompareTag("LockedDoor"))
                if (o.GetComponent<Transform>().rotation != qRead.Read<Quaternion>("LockedDoor: " + o.name))
                {
                    o.GetComponent<Transform>().rotation = qRead.Read<Quaternion>("LockedDoor: " + o.name);
                    o.GetComponent<OpenLockedDoor>().doorOpened = !o.GetComponent<OpenLockedDoor>().doorOpened;
                }
        }
        IM.InventoryItems = new InventoryItemController[IM.InventoryItems.Count()];
        foreach (Transform i in IM.ItemContent.transform)
            GameObject.Destroy(i.gameObject);
        IM.Items = InventoryItems;
        characterController.loadlock = true;
        character.position = pos;
        screen.SetActive(false);
        CursorVisibility.Instance.Close();
    }
}
