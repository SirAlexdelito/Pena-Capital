using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EscenaJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void EscenaOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void EscenaCreditos()
    {
        SceneManager.LoadScene("creditos");
    }

    public void Salir()
    {
        Application.Quit();
    }

}

