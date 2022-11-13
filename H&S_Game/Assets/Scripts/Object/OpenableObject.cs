using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : InteractableObject
{

    protected bool isOpen = false;

    public override void onBeingInteracted(EscapeeInteractComponent escapeeInteractComponent)
    {
        if (escapeeInteractComponent.CanOpen)
        {
            isOpen = !isOpen;
            onInteract?.Invoke();
            Debug.Log(gameObject.name + ", open state: " + isOpen);
        }
    }

    public override void onBeingInteracted(MonsterInteractComponent monsterInteractComponent)
    {
        throw new System.NotImplementedException();
    }

    public override void onMouseDrag()
    {

    }
}
