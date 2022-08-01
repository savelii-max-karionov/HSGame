using UnityEngine;

public class HidingObject : InteractableObject
{
    [SerializeField] private BoxCollider2D hideCollider;
    [SerializeField] private GameObject playerVisual;
    //[SerializeField] private Animator animator;
    private float mouseHoldTime = 0f;
    private const float holdThreshold = 2f;
    private bool isHiden = false;
    private bool hasChangedHidenState = false;



    // Start is called before the first frame update
    void Start()
    {
    }

    public override void mouseDown()
    {
        if (mouseHoldTime < holdThreshold)
        {
            base.mouseDown();
        }
        hasChangedHidenState = false;
        mouseHoldTime = 0f;
    }
    public override void mouseDrag()
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
