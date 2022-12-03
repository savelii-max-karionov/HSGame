using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ReapplyRootMotion : MonoBehaviour
{
    public GameObject targetObject;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnAnimatorMove()
    {
        if (targetObject != null && animator != null)
        {
            targetObject.transform.position += animator.deltaPosition;
        }
    }
}
