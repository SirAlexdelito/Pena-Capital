using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadButton1 : MonoBehaviour
{
    private Button button;
    private SaveGame loadSystem;
    void Start(){
        var a = Resources.FindObjectsOfTypeAll<GameObject>();
        if(GameObject.FindGameObjectWithTag("Save") is null) 
            foreach(var o in a )
                if(o.CompareTag("Persistent")){
                    o.SetActive(true);
                    o.transform.Find("SaveSystem").gameObject.SetActive(true);
                }
    
        loadSystem = GameObject.FindGameObjectWithTag("Save").GetComponent<SaveGame>();

        button = GetComponent<Button>();
        button.onClick.AddListener(loadSystem.load); 
    }
}
