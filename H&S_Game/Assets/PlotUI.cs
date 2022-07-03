using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotUI : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public int index;
    EscapeeComponent escapee;

    //public void Start()
    //{
    //    foreach (var i in PlayerComponent.playerList)
    //    {
    //        var photonView = i.GetComponent<Photon.Pun.PhotonView>();
    //        if (photonView && photonView.IsMine)
    //        {
    //            escapee = i.GetComponent<EscapeeComponent>();
    //            inventoryManager = escapee.inventoryManager;
    //        }
    //    }
       
    //}

    //private void OnMouseDown()
    //{
    //    inventoryManager.getSlots()[index].getGadgetStack().gadget.OnUse(escapee);
    //}
}
