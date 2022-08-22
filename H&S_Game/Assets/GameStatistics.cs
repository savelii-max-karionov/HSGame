using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public static List<GameObject> playerList = new List<GameObject>();

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
}
