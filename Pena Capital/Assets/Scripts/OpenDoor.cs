using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenDoor : MonoBehaviour
{
    public bool doorOpened;
    public string numero;
    private bool coroutineAllowed;
    private float a;
    private float b;
    private float c;
    public float g;
    public GameObject Inventory;
    //public GameObject doorWing; 
    public Quaternion startingPos { get; private set; }
    public Quaternion actualPos { get; private set; }
    // Start is called before the first frame update
    private bool inRange;
    void Awake()
    {
        doorOpened = false;
        coroutineAllowed = true;
        inRange = false;
        a = transform.localRotation.eulerAngles.x;
        b = transform.localRotation.eulerAngles.y;
        c = transform.localRotation.eulerAngles.z;
        foreach (GameObject x in Resources.FindObjectsOfTypeAll<GameObject>())
            if (((GameObject)x).CompareTag("Inventory"))
                Inventory = x;
    }
    public void Update(){
        FirstPersonController characterController = GameObject.FindGameObjectWithTag("FirstPersonController").GetComponent<FirstPersonController>();
        if(Vector3.Distance(transform.position, characterController.transform.position) <= 3)
        {inRange=true;}
        else{inRange=false;}
    }
    public bool IsOpen()
    {
        if (actualPos != startingPos)
            return true;
        else return false;
    }
    public void OpenStatic()
    {
        doorOpened = true;
        transform.localRotation = Quaternion.Euler(a, g, c);
    }
    private void OnMouseDown()
    {
        if (inRange){
        Invoke("RunCoroutine", 0f);
        }
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
                            actualPos = Quaternion.Euler(a, +i, c);
                            GameObject.FindGameObjectWithTag("DoorSound").GetComponent<AudioSource>().Play();
                            yield return new WaitForSeconds(0f);
                        }
                        SavingElements.Instance.doors.Add(this);
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
                            actualPos = Quaternion.Euler(a, +i, c);
                            GameObject.FindGameObjectWithTag("DoorSound").GetComponent<AudioSource>().Play();
                            yield return new WaitForSeconds(0f);
                        }
                        SavingElements.Instance.doors.Add(this);
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
    void OnMouseOver()
    {
        if (inRange){
            if (!doorOpened)
                {
                    if (!Inventory.activeSelf)
                    {DisplayText.Instance.changeText("Abrir puerta " + numero);}
                    else{
                    DisplayText.Instance.changeText("");
                    }
                }
                else{
                    DisplayText.Instance.changeText("");
                }
        }
        else{
            DisplayText.Instance.changeText("");
        }
    }
    void OnMouseExit()
    {
        DisplayText.Instance.changeText("");
    }
}