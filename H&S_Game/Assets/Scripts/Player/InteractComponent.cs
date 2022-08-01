using UnityEngine;

public class InteractComponent : MonoBehaviour
{

    public Collider2D interactCollider;
    public GameObject visualObject;
    public GameObject mainObject;


    private Camera cam;
    private PlayerComponent playerComponent;


    private void Awake()
    {
        cam = Camera.main;
        playerComponent = GetComponent<PlayerComponent>();
    }
    private void Update()
    {
        Collider2D[] colliders = new Collider2D[10];
        // If the player/monster click down and there is an interactable object within rnage.
        if (Input.GetMouseButtonDown(0) && interactCollider.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)))
        {

            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.transform != null)
            {
                var hitObject = rayHit.transform.gameObject;
                var interactObj = hitObject.GetComponent<InteractableObject>();
                var gadget = hitObject.GetComponent<GadgetComponent>();
                if (interactObj != null)
                {
                    interactObj.registerHidingEvent(hide);
                    interactObj.registerAppearingEvent(appear);
                    interactObj.mouseDown();
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
                    interactObj.mouseDrag();

                }
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
    public void appear()
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
