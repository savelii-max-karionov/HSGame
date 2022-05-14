using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private new void OnMouseUp()
    {
        if (mouseHoldTime < holdThreshold)
        {
            base.OnMouseUp();
        }       
        hasChangedHidenState = false;
        mouseHoldTime = 0f;
    }
    private void OnMouseDrag()
    {
        mouseHoldTime += Time.deltaTime;
        if(!hasChangedHidenState && isOpen && mouseHoldTime > holdThreshold)
        {
            if (!isHiden)
            {
                isHiden = true;
                Debug.Log("hiding into " + gameObject.name);
                hasChangedHidenState = true;
            }
            else
            {
                isHiden = false;
                Debug.Log("comming out from " + gameObject.name);
                hasChangedHidenState = true;
            }
        }
        
    }
}
