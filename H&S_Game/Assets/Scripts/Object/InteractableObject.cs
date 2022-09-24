using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{

    public Action onOpen;
    public Action OnHiden;
    public Action onAppear;
    public Action<TunnelingObject, bool> onTunneling;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void onMouseDown(EscapeeInteractComponent escapeeInteractComponent);

    public abstract void onMouseDown(MonsterInteractComponent monsterInteractComponent);

    public abstract void onMouseDrag();

    public void registerHidingEvent(Action action)
    {
        OnHiden += action;
    }


    public void deregisterHidingEvent(Action action)
    {
        OnHiden -= action;
    }

    public void registerTunnelingEvent(Action<TunnelingObject, bool> action)
    {
        onTunneling += action;
    }

    public void deregisterTunnelingEvent(Action<TunnelingObject, bool> action)
    {
        onTunneling -= action;
    }
    public void registerAppearingEvent(Action action)
    {
        onAppear += action;
    }

    public void deregisterAppearingEvent(Action action)
    {
        onAppear -= action;
    }

    protected void invokeHiddingEvent()
    {
        OnHiden?.Invoke();
    }

    protected void invokeAppearingEvent()
    {
        onAppear?.Invoke();
    }

    protected virtual void invokeTunnelingEvent()
    {

    }

    public void emptyHidingEvnet()
    {
        OnHiden = null;
    }
    public void emptyAppearingEvent()
    {
        onAppear = null;
    }
    public void emptyTunnelingEvent()
    {
        onTunneling = null;
    }

}
