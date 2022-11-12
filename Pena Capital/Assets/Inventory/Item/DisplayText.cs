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
    void Awake()
    {
        Instance=this;
        Text=GameObject.FindGameObjectWithTag("DisplayText").GetComponent<TextMeshProUGUI>();
        Display=GameObject.FindGameObjectWithTag("DisplayText").GetComponent<Canvas>();
    }

    public void changeText(string t){
        Text.text=t;
    }
    void Update()
    {
        
    }
}
