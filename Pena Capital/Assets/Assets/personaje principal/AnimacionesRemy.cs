using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesRemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        transform.Rotate(0, x * TimeDeltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * TimeDeltaTime * velocidadMovimiento);
    }
}
