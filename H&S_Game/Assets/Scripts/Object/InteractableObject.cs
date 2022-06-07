using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    protected bool isOpen = false;
    public delegate Action openHandler();
    public event openHandler onOpen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void mouseUp()
    {
        isOpen = !isOpen;
        if (isOpen) onOpen?.Invoke();
        Debug.Log(gameObject.name + ", open state: " + isOpen);
    }


}
