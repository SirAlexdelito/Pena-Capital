using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : Action
{
    public Transform character;
    override
    public IEnumerator Act(){
        SceneManager.LoadScene("Casa");
        return null;
    }
}
