using UnityEngine;

public class HidingObject : OpenableObject
{
    public bool needOpen = false;
    //[SerializeField] private Animator animator;
    protected float mouseHoldTime = 0f;
    protected const float holdThreshold = 2f;
    protected bool isHiden = false;
    private bool hasChangedHidenState = false;



    public override void onMouseDown()
    {
        if (needOpen && mouseHoldTime < holdThreshold)
        {
            isOpen = !isOpen;
            onOpen?.Invoke();
            Debug.Log(gameObject.name + ", open state: " + isOpen);
        }
        hasChangedHidenState = false;
        mouseHoldTime = 0f;
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
                invokeHiddingEvent();
                emptyHidingEvnet();
                hasChangedHidenState = true;
            }
            else
            {
                isHiden = false;
                Debug.Log("comming out from " + gameObject.name);
                invokeAppearingEvent();
                emptyAppearingEvent();
                hasChangedHidenState = true;
            }
            isOpen = false;
        }

    }



}
