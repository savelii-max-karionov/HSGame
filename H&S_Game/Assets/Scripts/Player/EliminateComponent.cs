using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateComponent : InteractComponent
{
    protected override void OnInteract(GameObject hitObject, EscapeeComponent escapee)
    {
        var escapeeInteractComponent = hitObject.GetComponent<EscapeeInteractComponent>();

        if (escapeeInteractComponent != null)
        {
            
        }
    }
}
