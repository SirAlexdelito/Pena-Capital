using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesRemy : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;

    public Animator anim;//Necesitareis esto
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool saltar; //Necesitareis esto
    public float velocidadInicial;


    void Start()
    {
        saltar = false; //Necesitareis esto
        anim = GetComponent<Animator>(); //Necesitareis esto
    }
    void update(){ 
        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
       
        anim.SetFloat("VelX", x);//Necesitareis esto
        anim.SetFloat("VelY", y);//Necesitareis esto

     //   if (saltar==true) {//Necesitareis esto
       // if(Input.GetKeyDown(KeyCode.Space))
      //  {
       //     anim.setBool("saltar"),true);
      //      rb.AddForce(new Vector3 (0, fuerzaDeSalto, 0). ForceMode.Impulse);
       // }else { andar...

    }

}

