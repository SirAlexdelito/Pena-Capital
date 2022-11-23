using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CI.QuickSave;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveGame : MonoBehaviour
{
    public void save()
    {
        try{
            var s = (Application.persistentDataPath.ToString() + "\\QuickSave\\");
            var dir = Directory.CreateDirectory(s);
            foreach(FileInfo d in  dir.GetFiles()) 
            d.Delete();
        }catch(Exception e){
            Console.WriteLine("asdf");
        }
        var character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        var IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        var qSave = QuickSaveWriter.Create("DATA");
        qSave.Write("Position", character.position);
        foreach (var i in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (i.CompareTag("PickeableObject"))
                qSave.Write("Ilevel: " + i.name, i.name);
            if (i.CompareTag("Door"))
                qSave.Write("Door: " + i.name, i.GetComponent<Transform>().rotation);
            if (i.CompareTag("LockedDoor"))
                qSave.Write("LockedDoor: " + i.name, i.GetComponent<Transform>().rotation);
        }
        IM.Items.ForEach(i => qSave.Write("Iinventory: " + i.name, i));
        qSave.Commit();
    }
    public void load()
    {
        var character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        var characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        var IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        var qRead = QuickSaveReader.Create("DATA");
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
        foreach(Transform i in IM.ItemContent.transform) 
            GameObject.Destroy(i.gameObject);
        IM.Items = InventoryItems;
        characterController.loadlock=true;
        character.position = pos;
    }
}