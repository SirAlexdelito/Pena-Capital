using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadButton1 : MonoBehaviour
{
    private Button button;
    private SaveGame loadSystem;
    void Start(){
        foreach(GameObject g in Resources.FindObjectsOfTypeAll<GameObject>())
            if(g.CompareTag("Save")) loadSystem = g.GetComponent<SaveGame>();
        button = GetComponent<Button>();
        button.onClick.AddListener(loadSystem.load); 
    }
}
