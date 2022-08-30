using UnityEngine;

public class Slot
{
    public bool isEmpty = true;
    GadgetStack stack;
    GameObject gadgetGameObj;


    public Slot()
    {
        isEmpty = true;
        stack = new GadgetStack();
    }

    public void setGadgetStack(GadgetData gadget, int num)
    {
        isEmpty = false;
        stack.gadget = gadget;
        stack.num = num;
    }

    public void setObj(GameObject obj)
    {
        gadgetGameObj = obj;
    }

    /// <summary>
    /// Empty the slot.
    /// </summary>
    /// <returns></returns>
    public GadgetStack removeAllGadgets()
    {
        isEmpty = true;
        GadgetStack ret = stack;
        stack.gadget = null;
        stack.num = 0;
        gadgetGameObj = null;
        return ret;
    }

    public GadgetData removeGadget(int num)
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
        return gadgetGameObj;
    }
}

