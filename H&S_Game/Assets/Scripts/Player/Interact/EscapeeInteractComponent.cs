using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using static ClimbingTunnelingObject;

public class EscapeeInteractComponent : InteractComponent
{
    public AnimationEventDispatcher animationEventDispatcher;

    bool canGrab = true;
    bool canOpen = true;
    bool canHide = true;
    bool canUseGadgets = true;
    bool canTunneling = true;
    bool canCollectGadgets = true;
    bool canHop = true;
    private bool isTransporting = false;
    private float timeElaspedForTunneling;

    public bool CanGrab { get => canGrab; }
    public bool CanOpen { get => canOpen; }
    public bool CanHide { get => canHide; }
    public bool CanUseGadgets { get => canUseGadgets; }
    public bool CanTunneling { get => canTunneling; }
    public bool CanCollectGadgets { get => canCollectGadgets; }
    public bool CanHop { get => canHop; }




    private TunnelingObject tunnelingObject;

    private void Start()
    {
        if(movementComponent == null) Debug.Assert(movementComponent,"movementComponent not found");
        PhotonNetwork.AddCallbackTarget(this);
    }

    private new void Update()
    {
        base.Update();
        HandleTransporting();
        
    }

    public void onHoppingEnd(string animationName)
    {

        // need to reset animator booleans in late update because we must give a chance for animator to update its state in the statemachine.
        switch (animationName)
        {
            case "MediumHoppingMovement":
                animator.SetBool("isHoppingMedium", false);
                Debug.Log("onHoppingEnd : " + animationName);
                mainObject.transform.localScale = Vector3.one;
                movementComponent.resetHoppingState();
                animationEventDispatcher.clearAllEvents();
                break;
            case "MediumHoppingDownMovement":
                animator.SetBool("isHoppingDownMedium", false);
                Debug.Log("onHoppingEnd : " + animationName);
                mainObject.transform.localScale = Vector3.one;
                movementComponent.resetHoppingState();
                animationEventDispatcher.clearAllEvents();
                break;
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
                movementComponent.enabled = true;

                Transform outputPointObj = tunnelingObject.output.transform.Find("Output Point");
                if (outputPointObj != null)
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
            registerCallbacksAccordingToTag(interactObj.mainObject.tag, interactObj);

            interactObj.onBeingInteracted(this);
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

    void onHopping()
    {
        canGrab = false;
        canOpen = false;
        canHide = false;
        movementComponent.IsHopping = true;
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

    void registerCallbacksAccordingToTag(string tag, InteractableObject interactable)
    {
        switch (tag)
        {
            case "OpenableObject":
                interactable.registerInteractEvent(open);
                break;
            case "HideableObject":
                interactable.registerInteractEvent(open);
                interactable.registerDragEvent(hide,unhide);
                break;
            case "TransportPortal":
                var tunnelingObj = (TunnelingObject)interactable;
                Action tunnelingCallback = () =>
                {
                    tunneling(tunnelingObj);
                };
                interactable.registerInteractEvent(tunnelingCallback);
                break;
                
        }
    }

        /// <summary>
    /// what the player behave when it's entering a vent.
    /// </summary>
    public void tunneling(TunnelingObject tunnelingObject)
    {
        Debug.Log("tunneling");
        this.tunnelingObject = tunnelingObject;
        // Trigger entering event
        // TODO

        visualObject.SetActive(false);

        // movement of invisible player object
        if (!movementComponent)
        {
            Debug.LogError("Unable to disable movement of the player when tunneling.");
        }
        movementComponent.enabled = false;
        isTransporting = true;
        timeElaspedForTunneling = 0;
        // appear after timeElasped has passed the required transporting time. It will be implemented in the Update.
    }

    public void hop(HeightOptions option, FaceDirection direction)
    {
        onHopping();

        if (animationEventDispatcher)
        {
            animationEventDispatcher.registerAnimationCompleteEvent(onHoppingEnd);
        }

        if (direction == FaceDirection.left)
        {
            mainObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        switch (option)
        {
            case HeightOptions.medium:
                animator.SetBool("isHoppingMedium",true);
                movementComponent.setOutOfLevel(true);
                break;
            case HeightOptions.downMedium:
                animator.SetBool("isHoppingDownMedium", true);
                movementComponent.setOutOfLevel(false);
                break;
        }
    }
}
