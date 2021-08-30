using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D hideCollider;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private Animator animator;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    public void Open()
    {
        open = !open;
        animator.SetBool("Open", open);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
