using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCellDoor : MonoBehaviour
{
    private bool doorOpened;
    private bool coroutineAllowed;
    public float time = 2f;
    public Vector3 startingPos;
    public Vector3 finalPos;

    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        Vector3 startingPos  = transform.position;
        Vector3 finalPos = transform.position + (transform.right * 5);

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
        if(!doorOpened){
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            doorOpened = true;
        }else{
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(finalPos, startingPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            doorOpened = false;
        }
        coroutineAllowed = true;
    }
}
