using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;
    public List<GadgetComponent> craftableList = new List<GadgetComponent>();

    public void Start()
    {
        var gadgets = Resources.LoadAll<GadgetComponent>("Gadgets");
        foreach (var gadget in gadgets)
        {
            if (gadget.gadget.isCraftable)
            {
                craftableList.Add(gadget);
            }
        }
    }

    public CraftingManager Instance { get { return instance; } }

    public GameObject craft(GadgetComponent gadget1, GadgetComponent gadget2)
    {
        GadgetComponent foundGadgetComponent = null;
        if (gadget1 && gadget2)
        {
            string id1 = gadget1.gadget.id;
            string id2 = gadget2.gadget.id;
            bool is_id1_found = false;
            bool is_id2_found = false;
            foreach (GadgetComponent craftableGadgetComponent in craftableList)
            {
                foreach(var ingredient_id in craftableGadgetComponent.gadget.recipe)
                {
                    if(ingredient_id == id1)
                    {
                        is_id1_found = true;
                    }
                    else if(ingredient_id == id2)
                    {
                        is_id2_found = true;
                    }
                }
                if (is_id1_found && is_id2_found)
                {
                    foundGadgetComponent = craftableGadgetComponent;
                }
            }
        }

        if (!foundGadgetComponent) return null;       
        var craftedObj = PhotonNetwork.Instantiate("Gadgets/"+foundGadgetComponent.name, Vector3.zero, Quaternion.identity);
        craftedObj.gameObject.SetActive(false);
        return craftedObj.gameObject;   
    }

}
