using UnityEngine;

public class HidingObject : InteractableObject
{
    public bool needOpen = false;
    //[SerializeField] private Animator animator;
    protected float mouseHoldTime = 0f;
    protected const float holdThreshold = 2f;
    protected bool isHiden = false;
    private bool hasChangedHidenState = false;
    protected bool isOpen = false;


    public override void onBeingInteracted(EscapeeInteractComponent escapeeInteractComponent)
    {
        if (escapeeInteractComponent.CanOpen)
        {
            if (needOpen && mouseHoldTime < holdThreshold)
            {
                isOpen = !isOpen;
                onInteract?.Invoke();
                Debug.Log(gameObject.name + ", open state: " + isOpen);
            }
            hasChangedHidenState = false;
            mouseHoldTime = 0f;
        }
    }

    public override void onMouseDrag()
    {
        mouseHoldTime += Time.deltaTime;
        if (!hasChangedHidenState && mouseHoldTime > holdThreshold)
        {
            if (!isHiden)
            {
                isHiden = true;
                Debug.Log("hiding into " + gameObject.name);
                onDragBegin?.Invoke();
                hasChangedHidenState = true;
            }
            else
            {
                isHiden = false;
                Debug.Log("comming out from " + gameObject.name);
                onDragEnd?.Invoke();
                emptyDragEvent();
                hasChangedHidenState = true;
            }
            isOpen = false;
        }

    }

    public override void onBeingInteracted(MonsterInteractComponent monsterInteractComponent)
    {
        throw new System.NotImplementedException();
    }
}
