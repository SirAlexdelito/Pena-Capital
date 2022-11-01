using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InteractableObject", menuName = "InteractableObject/Create New InteractableObject")]
public class InteractableObject : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public void Use() { }
}
