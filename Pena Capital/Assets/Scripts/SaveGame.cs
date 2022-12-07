using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CI.QuickSave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveGame : MonoBehaviour
{

    public void save()
    {
        try
        {
            var s = (Application.persistentDataPath.ToString() + "\\QuickSave\\");
            var dir = Directory.CreateDirectory(s);
            foreach (FileInfo d in dir.GetFiles())
                d.Delete();
        }
        catch (Exception e)
        {
            Console.WriteLine("asdf");
        }
        var screen = GameObject.FindGameObjectWithTag("Screen");
        var character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        var IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        var qSave = QuickSaveWriter.Create("DATA");
        qSave.Write("Scene", SceneManager.GetActiveScene().buildIndex);
        qSave.Write("Position", character.position);
        SavingElements.Instance.lockedDoors.ForEach(x => { if (x != null) qSave.Write("LockedDoor: " + x.gameObject.name, x.doorOpened); });
        SavingElements.Instance.doors.ForEach(x => { if (x != null) qSave.Write("Door: " + x.gameObject.name, x.doorOpened); });
        SavingElements.Instance.pickedItems.ForEach(x => { if (x != null) qSave.Write("PickeableObject: " + x, x); });
        IM.Items.ForEach(i => qSave.Write("Iinventory: " + i.name, i));
        qSave.Commit();
        screen.SetActive(false);
        CursorVisibility.Instance.Close();
    }
    public void load()
    {
        var qRead = QuickSaveReader.Create("DATA");
        int s = qRead.Read<int>("Scene");
        int i = SceneManager.GetActiveScene().buildIndex;
        var x = GameObject.FindGameObjectWithTag("Save");
        if (s != i)
            StartCoroutine(AsyncLoadScene(s));
        else loadSame();
    }

    public void loadSame()
    {
        var screen = GameObject.FindGameObjectWithTag("Screen");
        var character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        var characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        var IM = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        var qRead = QuickSaveReader.Create("DATA");

        var pos = qRead.Read<Vector3>("Position");
        var levelItems = new List<string>();
        var InventoryItems = new List<Item>();

        qRead.GetAllKeys().ToList().ForEach(x =>
        {
            if (x.Contains("LockedDoor"))
            {
                var s = x.Substring(12, x.Length - 12);
                GameObject.Find(s).GetComponent<OpenLockedDoor>().OpenStatic();
                SavingElements.Instance.lockedDoors.Add(GameObject.Find(s).GetComponent<OpenLockedDoor>());
            }
            else if (x.Contains("Door"))
            {
                var s = x.Substring(6, x.Length - 6);
                GameObject.Find(s).GetComponent<OpenDoor>().OpenStatic();
                SavingElements.Instance.doors.Add(GameObject.Find(s).GetComponent<OpenDoor>());
            }
            else if (x.Contains("PickeableObject"))
            {
               var s = qRead.Read<string>(x);
                Destroy(GameObject.Find(s));
                SavingElements.Instance.pickedItems.Add(s);
            }
            else if (x.Contains("Iinventory"))
            {
                InventoryItems.Add(qRead.Read<Item>(x));
            }
        });

        IM.InventoryItems = new InventoryItemController[IM.InventoryItems.Count()];
        foreach (Transform i in IM.ItemContent.transform)
            GameObject.Destroy(i.gameObject);
        IM.Items = InventoryItems;
        characterController.loadlock = true;
        character.position = pos;
        if (screen is not null) screen.SetActive(false);
        if (CursorVisibility.Instance is not null)
            CursorVisibility.Instance.Close();
    }
    public IEnumerator AsyncLoadScene(int scene)
    {
        yield return SceneManager.LoadSceneAsync(scene);
        loadSame();
    }
}