using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GadgetUIComponent : EventTrigger
{
    public int index;
    public float draggingThresholdTime = 0.5f;
    public Canvas canvas;

    float clickedTime = 0f;
    GameObject draggingObj;
    Image image;
    EscapeeComponent controlledPlayer;
    GameObject gadgetObject;
    InventoryManager inventoryManager;
    CraftingManager craftingManager;
    bool isDragging = false;


    public GameObject GadgetObject { get => gadgetObject; set => gadgetObject = value; }
    public Image Image { get => image; set => image = value; }

    //If this function is Start, it will not be called after scene is loaded..
    private void OnEnable()
    {
        Image = GetComponent<Image>();
        craftingManager = FindObjectOfType<CraftingManager>();
        canvas = FindObjectOfType<Canvas>();

        foreach (var player in PlayerComponent.playerList)
        {
            var photonView = player.GetComponent<Photon.Pun.PhotonView>();

            if (photonView && photonView.IsMine)
            {
                controlledPlayer = player.GetComponent<EscapeeComponent>();
                if (controlledPlayer)
                {
                    inventoryManager = controlledPlayer.inventoryManager;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            clickedTime += Time.deltaTime;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // If the dragging time is less than the threshold, nothing will happen.
        clickedTime = 0f;
        isDragging = true;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        if (clickedTime < draggingThresholdTime) return;

        if (!draggingObj)
        {
            createAndInitDraggingObj();
        }
        else
        {
            draggingObj.transform.position = Input.mousePosition;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);


        if (!gadgetObject)
        {
            Debug.LogError("Missing gadget data");
        }

        isDragging = false;
        if (clickedTime < draggingThresholdTime) return;

        var graphicsRaycaster = canvas.GetComponent<GraphicRaycasterComponent>();
        if (graphicsRaycaster)
        {
            var hoveredGadgetUIComponent = graphicsRaycaster.GetHoveredGadgetUIComponent();
            if(hoveredGadgetUIComponent != null)
            {
                var craftedGadgetObj = craftingManager.craft(gadgetObject.GetComponent<GadgetComponent>(), hoveredGadgetUIComponent.gadgetObject.GetComponent<GadgetComponent>());

                // Inventory
                if (craftedGadgetObj)
                {
                    inventoryManager.onCraft(index, hoveredGadgetUIComponent.index, craftedGadgetObj);
                }
                
            }
            else
            {
                dropItem();
            }
            if (draggingObj != null)
            {
                Destroy(draggingObj);
            }
        }

       
    }

    void dropItem()
    {
        var gadgetPhotonView = gadgetObject.gameObject.GetComponent<PhotonView>();
        if (!gadgetPhotonView)
        {
            Debug.LogError("Gadget doesnt contain PhotonView");
            return;
        }

        var anchor = controlledPlayer.transform.Find("DropAnchor");
        if (!anchor)
        {
            Debug.LogError("Cannot find the dropping anchor in the controlled object");
            return;
        }

        gadgetPhotonView.RPC("appear", RpcTarget.All, anchor.transform.position);
        inventoryManager.removeAll(index);
        clearGadgetUI();
    }

    private void createAndInitDraggingObj()
    {
        draggingObj = new GameObject("dragging object");
        draggingObj.transform.parent = canvas.transform;
        //The rendering order depends on the hirarchy.
        draggingObj.transform.SetAsLastSibling();

        var draggingImg = draggingObj.AddComponent<Image>();
        draggingImg.sprite = Image.sprite;
        draggingObj.transform.position = Input.mousePosition;
    }
    void clearGadgetUI()
    {
        gameObject.SetActive(false);
        image.sprite = null;
        gadgetObject = null;
    }

}
