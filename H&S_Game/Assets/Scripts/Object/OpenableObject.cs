using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : InteractableObject
{

    protected bool isOpen = false;

    public override void onMouseDown()
    {
        isOpen = !isOpen;
        onOpen?.Invoke();
        Debug.Log(gameObject.name + ", open state: " + isOpen);
    }

    public override void onMouseDrag()
    {

    }
}
