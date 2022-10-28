using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class CursorVisibility : MonoBehaviour
{
    public FirstPersonController Character;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        Character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            

            if (Cursor.visible == true)
            {
                Character.cursorvisible = false;
                Character.m_MouseLook.XSensitivity = 2;
                Character.m_MouseLook.YSensitivity = 2;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Character.cursorvisible = true;
                Character.m_MouseLook.XSensitivity = 0;
                Character.m_MouseLook.YSensitivity = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
