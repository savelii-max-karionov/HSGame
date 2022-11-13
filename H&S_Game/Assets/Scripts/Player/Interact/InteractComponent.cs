using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractComponent : MonoBehaviour
{

    public Collider2D interactCollider;
    public GameObject visualObject;
    public GameObject mainObject;
    protected Animator animator;

    private Camera cam;
    private PlayerComponent playerComponent;
    protected PlayerMovement movementComponent;

    private PhotonView photonView;
    

    private void Awake()
    {
        cam = Camera.main;
        playerComponent = GetComponent<PlayerComponent>();
        photonView = mainObject.GetPhotonView();
        if (!photonView)
        {
            Debug.Log("PhotonView not found");
        }
        animator = visualObject.GetComponent<Animator>();
    }

    protected void Update()
    {
        Collider2D[] colliders = new Collider2D[10];
        // If the player/monster click down and there is an interactable object within rnage.
        if (photonView.IsMine && Input.GetMouseButtonDown(0) && interactCollider.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)))
        {

            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.transform != null)
            {
                var hitObject = rayHit.transform.gameObject;

                OnInteract(hitObject);
            }
        }
        // If the player is holding the click and there is an interactable object within rnage.
        else if (Input.GetMouseButton(0) && interactCollider.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)) && mainObject.tag == "Escapee")
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.transform != null)
            {
                var interactObj = rayHit.transform.gameObject.GetComponent<InteractableObject>();
                if (interactObj != null)
                {
                    interactObj.registerDragEvent(hide,unhide);
                    interactObj.onMouseDrag();
                }
            }
        }
    }

    protected abstract void OnInteract(GameObject hitObject);

    public void open()
    {

    }
    
    public void hide()
    {
        visualObject?.SetActive(false);
        var movementComponent = mainObject?.GetComponent<PlayerMovement>();

        if (movementComponent != null)
        {
            movementComponent.enabled = false;
        }

        if (playerComponent != null)
        {
            playerComponent.IsHiding = true;
        }
        else
        {
            Debug.LogWarning("player object not found!");
        }
    }
    public void unhide()
    {
        visualObject?.SetActive(true);
        var movementComponent = mainObject?.GetComponent<PlayerMovement>();
        if (movementComponent != null)
        {
            movementComponent.enabled = true;
        }

        if (playerComponent != null)
        {
            playerComponent.IsHiding = false;
        }
        else
        {
            Debug.LogWarning("player object not found!");
        }
    }
}
