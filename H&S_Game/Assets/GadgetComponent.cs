using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetComponent : MonoBehaviour
{
    public Gadget gadget;
    ItemBarUI itemBar;
    // Start is called before the first frame update
    void Start()
    {
        itemBar = FindObjectOfType<ItemBarUI>();
        if (itemBar == null) Debug.LogWarning("ItemBarUI not found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        foreach(var i in PlayerComponent.playerList)
        {
            var photonView = i.GetComponent<Photon.Pun.PhotonView>();
            if (photonView&&photonView.IsMine)
            {
                var escapeeComponent = i.GetComponent<EscapeeComponent>();
                if (escapeeComponent != null)
                {
                    escapeeComponent.inventoryManager.addGadget(gadget,gameObject, 1);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
