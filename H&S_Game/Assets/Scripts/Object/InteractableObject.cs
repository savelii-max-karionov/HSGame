using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public GameObject mainObject;
    public Action onInteract;

    public Action onDragBegin;
    public Action onDragEnd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void onBeingInteracted(EscapeeInteractComponent escapeeInteractComponent);

    public abstract void onBeingInteracted(MonsterInteractComponent monsterInteractComponent);

    public abstract void onMouseDrag();


    public void registerInteractEvent(Action action)
    {
        onInteract += action;
    }

    public void emptyInteractEvent()
    {
        onInteract = null;
    }
    

    public void registerDragEvent(Action begin, Action end)
    {
        onDragBegin += begin;
        onDragEnd += end;
    }

    public void emptyDragEvent()
    {
        onDragBegin = null;
        onDragEnd = null;
    }

}
