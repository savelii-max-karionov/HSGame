using System.Collections;
using System.Collections.Generic;
using HS;
using UnityEngine;

public abstract class PlayerAnimationManager : MonoBehaviour
{
    private InputManager inputManager;
    Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        if (!animator) Debug.LogError(gameObject.name + ": Cannot find animator in children!");
        inputManager = FindObjectOfType<InputManager>();
        if (!inputManager) Debug.LogError("Cannot find the Input Manager");
    }

    protected virtual void Update()
    {
        if (animator != null && inputManager != null)
        {
            if(inputManager.horizontal > 0)
            {
                animator.SetTrigger("MovingRight");
                animator.ResetTrigger("Standing");
            }
            else if(inputManager.horizontal < 0)
            {
                animator.SetTrigger("MovingLeft");
                animator.ResetTrigger("Standing");
            }
            else
            {
                animator.SetTrigger("Standing");
                animator.ResetTrigger("MovingLeft");
                animator.ResetTrigger("MovingRight");
            }
        }
    }

}
