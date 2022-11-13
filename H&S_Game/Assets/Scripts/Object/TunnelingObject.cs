using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TunnelingObject : InteractableObject
{
    public GameObject input;
    public GameObject output;
    public float transportTime;
    protected bool disableVisualWhileTunneling;
    protected GameObject playerObject;
    protected Animator playerAnimator;

    private void OnEnable()
    {
        disableVisualWhileTunneling = true;
    }

    private bool findPlayer()
    {
        bool foundPlayer = false;
        foreach (var player in GameStatistics.playerList)
        {
            if (player.GetComponent<PhotonView>() && player.GetComponent<PhotonView>().IsMine)
            {
                playerObject = player;
                foundPlayer = true;
            }
        }


        if (!foundPlayer)
        {
            Debug.LogError("Cannot find player");
            return false;
        }

        return foundPlayer;
    }

    public override void onBeingInteracted(EscapeeInteractComponent escapeeInteractComponent)
    {
        if (escapeeInteractComponent.CanTunneling)
        {
            findPlayer();

            findAnimator();

            invokeTunnelingEvent();
        }
    }

    private bool findAnimator()
    {
        if (playerObject.GetComponentInChildren<Animator>())
        {
            playerAnimator = playerObject.GetComponentInChildren<Animator>();
            return true;
        }
        else
        {
            Debug.LogError("Tunnel cannot find the animator of the player!");
            return false;
        }
    }

    public override void onMouseDrag()
    {
        
    }

    private void invokeTunnelingEvent()
    {
        preTunneling();
        onInteract?.Invoke();
        postInvokeTunneling();
    }

    protected virtual void preTunneling()
    {

    }

    protected virtual void postInvokeTunneling()
    {
        
    }

    public override void onBeingInteracted(MonsterInteractComponent monsterInteractComponent)
    {
        throw new System.NotImplementedException();
    }
}
