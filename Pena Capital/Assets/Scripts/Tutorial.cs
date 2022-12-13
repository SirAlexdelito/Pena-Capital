using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int fase;
    bool corroutine;
    bool onw8;
    void Start()
    {
        DisplayText.Instance.changeTextTuto("");

        fase = 0;
        corroutine = true;
        onw8 = false;
    }
    void Update()
    {
        if (fase == 0 && (Input.anyKey))
        {
            try
            {
                GameObject.FindGameObjectWithTag("Comic").SetActive(false);
                DisplayText.Instance.changeTextTuto("Muevete con W A S D");

                StartCoroutine("w8");
            }
            catch (Exception) { }
        }
        if (fase == 1 && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
        {
            if (!onw8)
            {
                DisplayText.Instance.changeTextTuto("Prueba a coger un objeto de la mesa con click izquierdo");

                StartCoroutine("w8");


            }
        }
        else if (fase == 2 && InventoryManager.Instance.Items.Count > 0)
        {
            DisplayText.Instance.onTuto = true;
            DisplayText.Instance.changeTextTuto("El inventario se abre con la i ");
            fase++;
        }
        else if (fase == 3 && Input.GetButtonDown("OpenInventory"))
        {
            DisplayText.Instance.changeTextTuto("Para seleccionar un objeto haz click en su icono");
            fase++;
        }
        else if (fase == 4 && InventoryManager.Instance.SelectedContent.childCount >= 1)
        {
            if (!corroutine) return;
            StartCoroutine("LastTuto");
        }
    }
    IEnumerator LastTuto()
    {
        corroutine = false;
        DisplayText.Instance.changeTextTuto("Al interactuar con el nivel lo haras con el objeto que tengas seleccionado");
        yield return new WaitForSeconds(3f);
        DisplayText.Instance.changeTextTuto("");
        yield return new WaitForSeconds(1f);
        DisplayText.Instance.Text.color = Color.red;
        DisplayText.Instance.changeTextTuto("Debo encontrar la salida...");
        DisplayText.Instance.Text.color = Color.white;
        yield return new WaitForSeconds(4f);
        DisplayText.Instance.onTuto = false;
        Destroy(this.gameObject);
    }
    IEnumerator w8()
    {
        onw8 = true;
        yield return new WaitForSeconds(3f);
        if (fase==1)
            DisplayText.Instance.onTuto = false;
        onw8 = false;
        fase++;
    }
}
