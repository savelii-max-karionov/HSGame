using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonManager : MonoBehaviourPun
{


    // Start is called before the first frame update
    void Awake()
    {
        if(GameStatus.IsMonster)
        {
            PhotonNetwork.Instantiate("MonsterObject", new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (!GameStatus.IsMonster)
        {
            PhotonNetwork.Instantiate("PlayerObject", new Vector3(0, 0, 0), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
