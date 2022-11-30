using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMenu : MonoBehaviour
{
    public GameObject screen;
    public GameObject opciones;
    public GameObject creditos;
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
        opciones.SetActive(true);
    }
    public void Countinuar()
    {
        screen.SetActive(false);
        CursorVisibility.Instance.Close();
    }
    public void EscenaCreditos()
    {
        creditos.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void VolverMainMenu()
    {
        screen.SetActive(true);
        opciones.SetActive(false);
        creditos.SetActive(false);
    }

}

