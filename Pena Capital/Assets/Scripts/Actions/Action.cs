using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public Item Item;
    public abstract IEnumerator Act();
    public void RunCoroutine()
    {
        StartCoroutine("Act"); 
    } 
}
