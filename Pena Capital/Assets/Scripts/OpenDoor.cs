using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool doorOpened;
    private bool coroutineAllowed;

    //public GameObject doorWing; 

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
    }

    private void OnMouseDown()
    {
        Invoke("RunCoroutine", 0f);
    }

    private void RunCoroutine()
    {
        StartCoroutine("OpenThatDoor");
    }

    private IEnumerator OpenThatDoor()
    {
        coroutineAllowed = false;
        if (!doorOpened)
        {
            for (float i = 175f; i <= 265f; i += 3f)
            {
                transform.localRotation = Quaternion.Euler(0f, +i, 0f);
                yield return new WaitForSeconds(0f);
            }
            doorOpened = true;
        }
        else
        {
            for (float i = 265f; i >= 175f; i -= 3f)
            {
                transform.localRotation = Quaternion.Euler(0f, +i, 0f);
                yield return new WaitForSeconds(0f);
            }
            doorOpened = false;
        }
        coroutineAllowed = true;
    }
}
