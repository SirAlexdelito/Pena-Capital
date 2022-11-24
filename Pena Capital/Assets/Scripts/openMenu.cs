using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class openMenu : MonoBehaviour
{
    public GameObject screen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            screen.SetActive(true);
            CursorVisibility.Instance.Open();
        }
    }

}
