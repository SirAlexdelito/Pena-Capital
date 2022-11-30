using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool doorOpened;
    private bool coroutineAllowed;
    private float a;
    private float b;
    private float c;
    public float g;
    public GameObject Inventory;
    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        a = transform.localRotation.eulerAngles.x;
        b = transform.localRotation.eulerAngles.y;
        c = transform.localRotation.eulerAngles.z;
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }

    private void OnMouseDown()
    {
        Invoke("RunCoroutine", 0f);
    }

    private void RunCoroutine()
    {
        if (coroutineAllowed)
        { StartCoroutine("OpenThatDoor"); }
    }

    private IEnumerator OpenThatDoor()
    {
        if (!Inventory.activeSelf)
        {
            if (!doorOpened)
            {
                coroutineAllowed = false;
                if (g >= 0)
                {
                    if (!doorOpened)
                    {
                        for (float i = b; i <= (b + g); i += 3f)
                        {
                            transform.localRotation = Quaternion.Euler(a, +i, c);
                            yield return new WaitForSeconds(0f);
                        }
                        doorOpened = true;
                    }
                    else
                    {
                        for (float i = (b + g); i >= b; i -= 3f)
                        {
                            transform.localRotation = Quaternion.Euler(a, +i, c);
                            yield return new WaitForSeconds(0f);
                        }
                        doorOpened = false;
                    }
                }
                else
                {
                    if (!doorOpened)
                    {
                        for (float i = b; i >= (b + g); i -= 3f)
                        {
                            transform.localRotation = Quaternion.Euler(a, +i, c);
                            yield return new WaitForSeconds(0f);
                        }
                        doorOpened = true;
                    }
                    else
                    {
                        for (float i = (b + g); i <= b; i += 3f)
                        {
                            transform.localRotation = Quaternion.Euler(a, +i, c);
                            yield return new WaitForSeconds(0f);
                        }
                        doorOpened = false;
                    }
                }
                coroutineAllowed = true;
            }
        }
    }
    void OnMouseEnter()
    {
        if (!doorOpened)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText("Abrir puerta");
    }
    void OnMouseExit()
    {
        if (!doorOpened)
            if (!Inventory.activeSelf)
                DisplayText.Instance.changeText("");
    }
}