using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/RestoreGadget", fileName = "new file")]
public class RestoreGadget : Gadget
{
    public float restoreAmount;


    // Start is called before the first frame update
    public override void OnUse(PlayerComponent target)
    {
        Debug.Log("using restore gadget");
    }
}
