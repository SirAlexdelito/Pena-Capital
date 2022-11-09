using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool doorOpened;
    private bool coroutineAllowed;
    private float a;
    private float b;
    private float c;
    public float g;

    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        a=transform.localRotation.eulerAngles.x;
        b=transform.localRotation.eulerAngles.y;
        c=transform.localRotation.eulerAngles.z;
    }

    private void OnMouseDown()
    {
        Invoke("RunCoroutine", 0f);
    }

    private void RunCoroutine()
    {
        if (coroutineAllowed)
            {StartCoroutine("OpenThatDoor");}
    }

    private IEnumerator OpenThatDoor()
    {
        coroutineAllowed = false;
        if (g>=0){
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
        else {
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