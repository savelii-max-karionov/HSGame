using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HidingObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D hideCollider;
    [SerializeField] private GameObject playerVisual;
    //[SerializeField] private Animator animator;
    private bool open;

    public delegate Action openHandler();
    public event openHandler onOpen;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    public void Open()
    {
        onOpen?.Invoke();
        open = !open;
        Debug.Log(gameObject.name + ", open state: " + open);
        //animator.SetBool("Open", open);
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
