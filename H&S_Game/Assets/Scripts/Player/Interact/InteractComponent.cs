using Photon.Pun;
using System;
using UnityEngine;

public abstract class InteractComponent : MonoBehaviour
{

    public Collider2D interactCollider;
    public GameObject visualObject;
    public GameObject mainObject;
    public event Action OnTransportEnd;
    protected Animator animator;

    private float timeElaspedForTunneling;


    private Camera cam;
    private PlayerComponent playerComponent;
    private PlayerMovement movementComponent;
    private bool isTransporting = false;
    private TunnelingObject tunnelingObject;
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

    private void Update()
    {
        HandleTransporting();

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
                    interactObj.onMouseDrag();
                }
            }
        }
    }

    protected abstract void OnInteract(GameObject hitObject);

    private void HandleTransporting()
    {
        if (isTransporting)
        {
            // Move the gameObject from input vent to output vent.
            mainObject.transform.position = Vector2.Lerp(tunnelingObject.input.transform.position, tunnelingObject.output.transform.position, timeElaspedForTunneling / tunnelingObject.transportTime);

            timeElaspedForTunneling += Time.deltaTime;
            if (timeElaspedForTunneling > tunnelingObject.transportTime)
            {
                isTransporting = false;

                // appear
                visualObject.SetActive(true);
                movementComponent.enabled = true;
                
                Transform outputPointObj = tunnelingObject.output.transform.Find("Output Point");
                if(outputPointObj != null)
                {
                    mainObject.transform.position = outputPointObj.position;
                }
                else
                {
                    Debug.LogError("Output point not found in output vent");
                }

                // play appear animation
                // TODO

                Debug.Log("End tunnelling");
                var movement = mainObject.GetComponent<PlayerMovement>();
                OnTransportEnd?.Invoke();

            }
        }
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


    /// <summary>
    /// what the player behave when it's entering a vent.
    /// </summary>
    public void tunneling(TunnelingObject tunnelingObject, bool disableVisual)
    {
        Debug.Log("tunneling");

        // Trigger entering event
        // TODO

        // disappear
        if (disableVisual)
        {
            visualObject.SetActive(false);
        }

        // movement of invisible player object
        movementComponent = mainObject.GetComponent<PlayerMovement>();
        if (!movementComponent)
        {
            Debug.LogError("Unable to disable movement of the player when tunneling.");
        }
        movementComponent.enabled = false;
        isTransporting = true;
        this.tunnelingObject = tunnelingObject;
        timeElaspedForTunneling = 0;

        // appear after timeElasped has passed the required transporting time. It will be implemented in the Update.

    }
}
