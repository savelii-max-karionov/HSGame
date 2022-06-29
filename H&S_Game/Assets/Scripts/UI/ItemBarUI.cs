using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBarUI : MonoBehaviour
{
    Animator animator;
    GameObject Handle;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            animator.SetBool("isOpen", true);
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }

}
