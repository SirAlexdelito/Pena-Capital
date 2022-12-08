using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Canvas Display;
    public static DisplayText Instance;
    public bool onTuto;
    void Awake()
    {
        Instance=this;
        Text=GameObject.FindGameObjectWithTag("DisplayText").GetComponent<TextMeshProUGUI>();
        Display=GameObject.FindGameObjectWithTag("DisplayText").GetComponent<Canvas>();
        onTuto=true;
    }

    public void changeText(string t){
        if(!onTuto)
            Text.text=t;
    }
    public void changeTextTuto(string t){
            Text.text=t;
    }
    void Update()
    {
        
    }
}
