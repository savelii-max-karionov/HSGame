using Photon.Pun;
using Photon.Realtime;
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
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    protected override void OnInteract(GameObject hitObject)
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

    [PunRPC]
    public void onInteractBegin(int viewId)
    {
        var escapeeRegidBody = mainObject.GetComponent<Rigidbody2D>();
        if (escapeeRegidBody)
        {
            escapeeRegidBody.simulated = false;
        }
        var targetParentView = PhotonView.Find(viewId);

        mainObject.transform.SetParent(targetParentView.GetComponent<MonsterInteractComponent>().InteractAnchor.transform);
        mainObject.transform.localPosition = new Vector3(0, 0, 0);
        visualObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    [PunRPC]
    public void OnBeingInteractedByMonster()
    {
        disableAbilityWhileCaptured();
    }

    private void disableAbilityWhileCaptured()
    {
        canGrab = false;
        canOpen = false;
        canHide = false;
    }

    public void onHopping()
    {
        canGrab = false;
        canOpen = false;
        canHide = false;
    }

    [PunRPC]
    public void onBeingExecutedProcessBegin()
    {
        // animation
        if (visualObject != null)
        {
            var spriteRenderer = visualObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(255f, 0f, 0f);
        }
    }

    [PunRPC]
    public void OnBeingkilledByMonster()
    {
        if (PhotonView.Get(this).IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            // Killed animation, turn to spectator
            PhotonNetwork.Instantiate("Spectator", mainObject.transform.position, Quaternion.identity);

            
        }
    }

    [PunRPC]
    public void onBeingReleasedFromExecuting(Vector3 releasePositon)
    {
        // TODO: Animation
        if(visualObject != null)
        {
            var escapeeRegidBody = mainObject.GetComponent<Rigidbody2D>();
            if (escapeeRegidBody)
            {
                escapeeRegidBody.simulated = true;
            }
            mainObject.transform.SetParent(null);
            mainObject.transform.position = releasePositon;
        }
        Debug.Log("I am saved from being killed");
        visualObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
