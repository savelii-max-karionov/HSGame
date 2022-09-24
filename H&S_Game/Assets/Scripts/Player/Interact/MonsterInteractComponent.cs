using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInteractComponent : InteractComponent
{
    protected override void OnInteract(GameObject hitObject, EscapeeComponent escapee)
    {
        var escapeeInteractComponent = hitObject.GetComponentInChildren<EscapeeInteractComponent>();

        if (escapeeInteractComponent != null)
        {
            escapeeInteractComponent.OnBeingInteractedByMonster();
        }
    }

}
