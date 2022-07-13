using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    ItemBarUI itemBarUI;
    List<Slot> inventory;
    const int inventorySize = 8;
    EscapeeComponent escapee;
    


    public InventoryManager(EscapeeComponent owner)
    {
        inventory = new List<Slot>(new Slot[inventorySize]);
        for(int i = 0; i < inventorySize; i++)
        {
            inventory[i] = new Slot();
        }
        itemBarUI = GameObject.FindObjectOfType<ItemBarUI>();
        escapee = owner;
    }

    /// <summary>
    /// Add gadget to the slot with minimum index.
    /// </summary>
    /// <param name="gadget"></param>
    public void addGadget(Gadget gadget,GameObject prefab, int num)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].isEmpty)
            {
                inventory[i].setGadgetStack(gadget,num);
                inventory[i].setPrefab(prefab);
                itemBarUI.refresh();
                return;
            }
        }

        Debug.Log("Inventory is full");
    }


    /// <summary>
    /// Remove num of gadgets at index. 
    /// If the index is invalid, return.
    /// If the num to be remove exceeds the number of gadgets at the slot, it will return false.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="num"></param>
    /// <returns>If the inventory does not contain enough gadget or the index is invalid, it will return false, otherwise it will return true.</returns>
    public bool removeGadget(int index, int num)
    {
        if(index < 0 || index >= inventorySize)
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + ": index not invalid");
            return false;
        }

        if (inventory[index].isEmpty || inventory[index].getGadgetStack().num < num)
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + ": slot is empty or the number exceeds the number of gadget");
            return false;
        }

        inventory[index].removeGadget(num);
        if (inventory[index].getGadgetStack().num == 0)
        {
            inventory[index].isEmpty = true;
        }

        itemBarUI.refresh();

        return true;
    }

    public bool removeAll(int index)
    {
        return removeGadget(index, inventory[index].getGadgetStack().num);
    }

    public void useGadget(EscapeeComponent escapee, int index)
    {
        if (index < 0 || index >= inventorySize)
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + ": index not invalid");
            return;
        }

        if (inventory[index].isEmpty )
        {
            Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name + ": slot is empty");
            return;
        }

        inventory[index].getGadgetStack().gadget.OnUse(escapee);
        removeGadget(index, 1);
    }


    public List<Slot> getSlots()
    {
        return inventory;
    }
}

