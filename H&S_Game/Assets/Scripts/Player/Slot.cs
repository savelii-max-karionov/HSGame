using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Slot
{
    public bool isEmpty = true;
    GadgetStack stack;
    GameObject prefab;


    public Slot()
    {
        isEmpty = true;
        stack = new GadgetStack();
    }

    public void setGadgetStack(Gadget gadget,int num)
    {
        isEmpty = false;
        stack.gadget = gadget;
        stack.num = num;
    }

    public void setPrefab(GameObject obj)
    {
        prefab = obj;
    }

    /// <summary>
    /// Empty the slot.
    /// </summary>
    /// <returns></returns>
    public GadgetStack removeAllGadgets()
    {
        isEmpty=true;
        GadgetStack ret = stack;
        stack.gadget = null;
        stack.num = 0;
        prefab = null;
        return ret;
    }

    public Gadget removeGadget(int num)
    {
        stack.num -= num;
        if (stack.num == 0)
        {
            removeAllGadgets();
        }
        return stack.gadget;
    }

    public GadgetStack getGadgetStack()
    {
        return stack;
    }

    public GameObject getPrefab()
    {
        return prefab;
    }
}

