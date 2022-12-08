using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int fase;
    bool corroutine;
    void Start()
    {
        DisplayText.Instance.changeTextTuto("Muevete con W A S D");
        fase = 0;
        corroutine=true;
    }
    void Update()
    {
        if (fase == 0 && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
        {
            DisplayText.Instance.changeTextTuto("Prueba a coger un objeto de la mesa con click izquierdo");
            fase++;
            DisplayText.Instance.onTuto=false;
        }
        else if(fase==1 && InventoryManager.Instance.Items.Count>0){
            DisplayText.Instance.onTuto=true;
            DisplayText.Instance.changeTextTuto("El inventario se abre con la i ");
            fase++;
        }
        else if(fase==1 && InventoryManager.Instance.Items.Count>0){
            DisplayText.Instance.changeTextTuto("El inventario se abre con la i ");
            fase++;
        }
        else if (fase == 2 && Input.GetButtonDown("OpenInventory"))
        {
            DisplayText.Instance.changeTextTuto("Para seleccionar un objeto haz click en su icono");
            fase++;
        }
        else if (fase == 3 && InventoryManager.Instance.SelectedContent.childCount==1)
        {
            if(!corroutine)return;
            StartCoroutine("LastTuto");
        }
    }
    IEnumerator LastTuto()
    {
        corroutine=false;
        DisplayText.Instance.changeTextTuto("Al interactuar con el nivel lo harás con el objeto que tengas seleccionado");
        yield return new WaitForSeconds(3f);
        DisplayText.Instance.changeTextTuto("");
        yield return new WaitForSeconds(1f);
        DisplayText.Instance.Text.color=Color.red;
        DisplayText.Instance.changeTextTuto("Debo encontrar la salida...");
        DisplayText.Instance.Text.color=Color.white;
        yield return new WaitForSeconds(4f);
        DisplayText.Instance.onTuto=false;
        Destroy(this.gameObject);
    }
}
