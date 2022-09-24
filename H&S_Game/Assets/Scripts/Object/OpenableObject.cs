using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : InteractableObject
{

    protected bool isOpen = false;

    public override void onMouseDown(EscapeeInteractComponent escapeeInteractComponent)
    {
        if (escapeeInteractComponent.CanOpen)
        {
            isOpen = !isOpen;
            onOpen?.Invoke();
            Debug.Log(gameObject.name + ", open state: " + isOpen);
        }
    }

    public override void onMouseDown(MonsterInteractComponent monsterInteractComponent)
    {
        throw new System.NotImplementedException();
    }

    public override void onMouseDrag()
    {

    }
}
