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
        // This component is enabled before players are joined, so we need to delay the initialization.
        //bool foundPlayer = findPlayer();
        //if (!foundPlayer)
        //{
        //    Debug.LogError("Cannot find the player in tunnel component");
        //    return;
        //}

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

    public override void onMouseDown()
    {
        findPlayer();

        findAnimator();

        invokeTunnelingEvent();
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

    protected override void invokeTunnelingEvent()
    {
        preTunneling();
        onTunneling?.Invoke(this,disableVisualWhileTunneling);
        postInvokeTunneling();
    }

    protected virtual void preTunneling()
    {

    }

    protected virtual void postInvokeTunneling()
    {

    }

}
