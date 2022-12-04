using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadButton : MonoBehaviour
{
    private Button button;
    private SaveGame loadSystem;
    void Start(){
        loadSystem = GameObject.FindGameObjectWithTag("Save").GetComponent<SaveGame>();
        button = GetComponent<Button>();
        button.onClick.AddListener(loadSystem.load); 
    }
}
