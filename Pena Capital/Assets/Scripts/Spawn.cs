using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform character = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<Transform>();
        character.position=new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
