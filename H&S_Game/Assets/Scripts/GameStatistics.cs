using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    // As long as the gameobject has a PlayerComponent, it is added into the playerList
    public static List<GameObject> playerList = new List<GameObject>();

    // Notice that it will not find all the controlled player.
    public static GameObject findControledPlayer()
    {
        foreach(GameObject player in playerList)
        {
            var photonView = player.GetComponent<PhotonView>();
            if(photonView && photonView.IsMine)
            {
                return player;
            }
        }
        Debug.LogError("Cannot find controlled player");
        return null;
    }

    public static void onlyEnableVisualOfTags(List<string> tags)
    {
        bool isSuccessful = true;

        foreach(var player in playerList)
        {
            if (!tags.Contains(player.tag))
            {
                if(!_setVisibility(player, false))
                {
                    isSuccessful = false;
                }
            }
            else
            {
                if (!_setVisibility(player, true))
                {
                    isSuccessful = false;
                }
            }
        }

        if (!isSuccessful)
        {
            Debug.LogWarning("Failed to set visibility. A non-player gameobject is added into the PlayerList");
        }
    }

    private static bool _setVisibility(GameObject player, bool isVisible)
    {
        var playerComp = player.GetComponent<PlayerComponent>();
        if (playerComp != null)
        {
            playerComp.visualObject.SetActive(isVisible);
            return true;
        }
        else
        {
            return false;
        }
    }
}
