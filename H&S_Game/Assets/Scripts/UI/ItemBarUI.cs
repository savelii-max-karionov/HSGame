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
    List<GadgetUIComponent> gadgetUIComponents;



    private void Start()
    {
        amIMonster();

        animator = GetComponent<Animator>();
        foreach (var i in GameStatistics.playerList)
        {
            // TODO: use GameStastatics
            var photonView = i.GetComponent<Photon.Pun.PhotonView>();
            if (photonView && photonView.IsMine)
            {
                escapee = i.GetComponent<EscapeeComponent>();
                // If the player is monster, dont show the item bar.
                if (escapee)
                {
                    inventoryManager = escapee.inventoryManager;
                }
                else
                {
                    gameObject.SetActive(false);
                    return;
                }

            }
        }

        if (inventoryManager == null)
        {
            Debug.LogError("Inventory manager not found");
        }

        gadgetUIComponents = new List<GadgetUIComponent>();
        var tempList = Slots.GetComponentsInChildren<GadgetUIComponent>();
        foreach (var gadgetUIComponent in tempList)
        {
            if (gadgetUIComponent.gameObject.tag == "Gadget")
            {
                gadgetUIComponents.Add(gadgetUIComponent);
            }
        }
        refresh();

        void amIMonster()
        {
            foreach (var i in GameStatistics.playerList)
            {
                var photonView = i.GetComponent<Photon.Pun.PhotonView>();
                if (photonView && photonView.IsMine)
                {
                    var escapeeComponent = i.GetComponent<EscapeeComponent>();
                    if (escapeeComponent == null)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
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
    /// </summaary>
    public void refresh()
    {
        Debug.Log("Refresh called");
        var slots = inventoryManager.getSlots();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].isEmpty)
            {
                gadgetUIComponents[i].GetComponent<Button>().onClick.RemoveAllListeners();
                gadgetUIComponents[i].gameObject.SetActive(false);
            }
            else
            {
                gadgetUIComponents[i].gameObject.SetActive(true);
                gadgetUIComponents[i].Image.sprite = slots[i].getGadgetStack().gadget.icon;
                gadgetUIComponents[i].GadgetObject = slots[i].getPrefab();
                int currentIndex = i;
                gadgetUIComponents[i].GetComponent<Button>().onClick.RemoveAllListeners();
                gadgetUIComponents[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log("button " + currentIndex + " triggered");
                    inventoryManager.useGadget(escapee, currentIndex);
                    if (inventoryManager.getSlots()[currentIndex].isEmpty)
                    {
                        Debug.Log("button " + currentIndex + " onclick removed");
                        gadgetUIComponents[currentIndex].GetComponent<Button>().onClick.RemoveAllListeners();
                    }

                });
            }

        }
    }

}
