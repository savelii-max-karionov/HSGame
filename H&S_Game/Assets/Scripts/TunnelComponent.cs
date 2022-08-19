using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class Tunnel : MonoBehaviour
{
    public GameObject input;
    public GameObject output;
    public float transportTime;
    public float interactDistance;
    GameObject playerObject;
    Animator playerAnimator;


    private void OnEnable()
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
            Debug.LogError("Cannot find the player in tunnel component");
            return;
        }

        if (playerObject.GetComponent<Animator>())
        {
            playerAnimator = playerObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Tunnel cannot find the animator of the player!");
            return;
        }

    }

    private void OnMouseDown()
    {
        // trigger animation

        // disappear

        // movement of invisible player object

        // appear

        // trigger animation

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
