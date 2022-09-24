using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeeInteractComponent : InteractComponent
{
    public PlayerMovement playerMovement;
    bool canGrab = true;
    bool canOpen = true;
    bool canHide = true;
    bool canUseGadgets = true;
    bool canTunneling = true;
    bool canCollectGadgets = true;

    public bool CanGrab { get => canGrab; }
    public bool CanOpen { get => canOpen; }
    public bool CanHide { get => canHide; }
    public bool CanUseGadgets { get => canUseGadgets; }
    public bool CanTunneling { get => canTunneling; }
    public bool CanCollectGadgets { get => canCollectGadgets; }

    private void Start()
    {
        if(playerMovement == null) Debug.Assert(playerMovement,"playerMovement not found");
    }

    protected override void OnInteract(GameObject hitObject, EscapeeComponent escapee)
    {
        var interactObj = hitObject.GetComponent<InteractableObject>();
        var gadget = hitObject.GetComponent<GadgetComponent>();
        if (interactObj != null)
        {
            // Describe how the character behave during the interaction.
            interactObj.registerHidingEvent(hide);
            interactObj.registerAppearingEvent(unhide);
            interactObj.registerTunnelingEvent(tunneling);

            interactObj.onMouseDown(this);
        }
        else if (gadget != null && CanCollectGadgets)
        {
            gadget.OnClicked();
        }
    }

    public void OnBeingInteractedByMonster()
    {
        Debug.Log("I am captured by the monster");

        disableAbilityWhileCaptured();


    }

    private void disableAbilityWhileCaptured()
    {
        canGrab = false;
        canOpen = false;
        canHide = false;
    }
}
