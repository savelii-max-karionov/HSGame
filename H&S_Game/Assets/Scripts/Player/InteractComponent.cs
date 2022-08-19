using Photon.Pun;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{

    public Collider2D interactCollider;
    public GameObject visualObject;
    public GameObject mainObject;

    private float timeElaspedForTunneling;


    private Camera cam;
    private PlayerComponent playerComponent;
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
                var interactObj = hitObject.GetComponent<InteractableObject>();
                var gadget = hitObject.GetComponent<GadgetComponent>();
                if (interactObj != null)
                {
                    // Describe how the character behave during the interaction.
                    interactObj.registerHidingEvent(hide);
                    interactObj.registerAppearingEvent(unhide);
                    interactObj.registerTunnelingEvent(tunneling);

                    interactObj.onMouseDown();
                }
                else if (gadget != null)
                {
                    gadget.OnClicked();
                }

            }
        }
        // If the player is holding the click and there is an interactable object within rnage.
        else if (Input.GetMouseButton(0) && interactCollider.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)) && mainObject.tag == "Player")
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
    public void tunneling(TunnelingObject tunnelingObject)
    {
        Debug.Log("tunneling");

        // Trigger entering event
        // TODO

        // disappear
        visualObject.SetActive(false);

        // movement of invisible player object
        isTransporting = true;
        this.tunnelingObject = tunnelingObject;
        timeElaspedForTunneling = 0;

        // appear after timeElasped has passed the required transporting time. It will be implemented in the Update.

    }
}
