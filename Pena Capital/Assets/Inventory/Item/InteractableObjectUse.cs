using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectUse : MonoBehaviour
{
    public InteractableObject IObject;
    void Use(int value) {
        if(IObject.value==value)
            IObject.Use();
    }
    private void OnMouseDown(int value) { Use(value); }
}
