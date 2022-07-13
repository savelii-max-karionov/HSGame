using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GadgetUIComponent : EventTrigger
{
    public int index;

    GameObject draggingObj;
    //Button button;
    Image image;
    EscapeeComponent controlledPlayer;
    GameObject gadgetObject;
    InventoryManager inventoryManager;

    public GameObject GadgetObject { get => gadgetObject; set => gadgetObject = value; }
    public Image Image { get => image; set => image = value; }

    // Start is called before the first frame update
    void Start()
    {
       Image = GetComponent<Image>();

        foreach(var player in PlayerComponent.playerList)
        {
            var photonView = player.GetComponent<Photon.Pun.PhotonView>();
            if (photonView && photonView.IsMine)
            {
                controlledPlayer = player.GetComponent<EscapeeComponent>();
                inventoryManager = controlledPlayer.inventoryManager;
            }
        }
        if(controlledPlayer == null)
        {
            Debug.LogError("Controlled player not found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    { 
        draggingObj = new GameObject("dragging object");
        draggingObj.transform.parent = GameObject.Find("Canvas").transform;
        var draggingImg = draggingObj.AddComponent<Image>();
        draggingImg.sprite = Image.sprite;
        draggingObj.transform.SetAsLastSibling(); 


    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(draggingObj != null)
        {
            //if(button != null)
            //{
            //    button.enabled = false;
            //}
            draggingObj.transform.position = Input.mousePosition;
            Debug.Log(draggingObj.transform.position);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (draggingObj != null)
        {
            Debug.Log("finally at"+draggingObj.transform.position);
            Destroy(draggingObj);
        }
        //if (button != null)
        //{
        //    button.enabled = true;
        //}

        if(GadgetObject == null)
        {
            Debug.LogError("Missing gadget data");
        }
        var newGadgetObj = GadgetObject.gameObject;
        newGadgetObj.SetActive(true);

        var anchor = controlledPlayer.transform.Find("DropAnchor");
        if(anchor != null)
        {
            newGadgetObj.transform.position = controlledPlayer.transform.Find("DropAnchor").transform.position;
            inventoryManager.removeAll(index);
            clearGadgetUI();
        }
    }

    void clearGadgetUI()
    {
        gameObject.SetActive(false);
        image.sprite=null;
        gadgetObject=null;
        draggingObj=null;
    }

}
