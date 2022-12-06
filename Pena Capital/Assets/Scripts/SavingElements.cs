using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingElements : MonoBehaviour
{
    public static SavingElements Instance;
    public List<OpenLockedDoor> lockedDoors;
    public List<string> pickedItems;
    public List<Item> inventory;
    public List<OpenDoor> doors;
    void Start(){
        doors = new List<OpenDoor>();
        lockedDoors = new List<OpenLockedDoor>();
        pickedItems = new List<string>();
        inventory = new List<Item>();
        Instance=this;
    }
}
