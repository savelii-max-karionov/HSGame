using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GadgetComponent : MonoBehaviour
{
    public Gadget gadget;
    ItemBarUI itemBar;
    PhotonView photonview;
    // Start is called before the first frame update
    void Start()
    {
        itemBar = FindObjectOfType<ItemBarUI>();
        if (itemBar == null) Debug.LogWarning("ItemBarUI not found");
        photonview = GetComponent<PhotonView>();

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
                    photonview.RPC("disappear", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    void disappear()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    void appear(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }

    
}
