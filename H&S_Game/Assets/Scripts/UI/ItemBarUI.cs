using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBarUI : MonoBehaviour
{
    public GameObject Slots;
    public Sprite defaultSlotImage;
    Animator animator;
    GameObject Handle;
    EscapeeComponent escapee;
    InventoryManager inventoryManager;
    List<Image> gadgetImgs;
    


    private void Start()
    {
        animator = GetComponent<Animator>();
        foreach (var i in PlayerComponent.playerList)
        {
            var photonView = i.GetComponent<Photon.Pun.PhotonView>();
            if (photonView && photonView.IsMine)
            {
                escapee = i.GetComponent<EscapeeComponent>();
                inventoryManager = escapee.inventoryManager;
            }
        }

        if(inventoryManager == null)
        {
            Debug.LogError("Inventory manager not found");
        }

        gadgetImgs = new List<Image>();
        var tempList = Slots.GetComponentsInChildren<Image>();
        foreach(var image in tempList)
        {
            if (image.gameObject.tag == "Gadget")
            {
                gadgetImgs.Add(image);
            }
        }
        refresh();
       
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            animator.SetBool("isOpen", true);
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }

    /// <summary>
    /// Refresh the Item Bar UI according to the gadgets in the Inve3ntory Manager.
    /// </summary>
    public void refresh()
    {
        var slots = inventoryManager.getSlots();
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].isEmpty)
            {
                gadgetImgs[i].gameObject.SetActive(false);
            }
            else
            {
                gadgetImgs[i].gameObject.SetActive(true);
                gadgetImgs[i].sprite = slots[i].getGadgetStack().gadget.icon;
                int currentIndex = i;
                gadgetImgs[i].GetComponent<Button>().onClick.AddListener(() => {
                    inventoryManager.useGadget(escapee, currentIndex);
                    if (inventoryManager.getSlots()[currentIndex].isEmpty)
                    {
                        gadgetImgs[currentIndex].GetComponent<Button>().onClick.RemoveAllListeners();
                    }
                    
                }); 
            }
            
        }
    }

}
