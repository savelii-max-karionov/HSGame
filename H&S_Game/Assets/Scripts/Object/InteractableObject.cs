using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected bool isOpen = false;
    public delegate Action openHandler();
    public event openHandler onOpen;

    public event Action OnHiden;
    public event Action onAppear;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void mouseDown()
    {
        isOpen = !isOpen;
        onOpen?.Invoke();
        Debug.Log(gameObject.name + ", open state: " + isOpen);
    }

    public virtual void mouseDrag()
    {

    }

    public void registerHidingEvent(Action action)
    {
        OnHiden += action;
    }

    public void deregisterHidingEvent(Action action)
    {
        OnHiden -= action;
    }

    public void registerAppearingEvent(Action action)
    {
        onAppear += action;
    }

    public void deregisterAppearingEvent(Action action)
    {
        onAppear -= action;
    }

    public void invokeHiddingEvent()
    {
        OnHiden?.Invoke();
    }

    public void invokeAppearingEvent()
    {
        onAppear?.Invoke();
    }

    public void emptyHidingEvnet()
    {
        OnHiden = null;
    }
    public void emptyAppearingEvent()
    {
        onAppear = null;
    }

}
