using Photon.Pun;
using UnityEngine;
public class PhotonManager : MonoBehaviourPun
{


    // Start is called before the first frame update
    void Awake()
    {
        if (GameStatus.IsMonster)
        {
            PhotonNetwork.Instantiate("MonsterObject", new Vector3(0, 0, 0), Quaternion.identity);

            //For testing
            var test1 = PhotonNetwork.Instantiate("PlayerObject", new Vector3(-5, 0, 0), Quaternion.identity);
            var test2 = PhotonNetwork.Instantiate("PlayerObject", new Vector3(-10, 0, 0), Quaternion.identity);
            test1.GetComponent<PlayerMovement>().enabled = false;
            test2.GetComponent<PlayerMovement>().enabled = false;
        }
        else if (!GameStatus.IsMonster)
        {
            PhotonNetwork.Instantiate("PlayerObject", new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            AddGadgetInScene();
        }

    }

    private void AddGadgetInScene()
    {
        PhotonNetwork.Instantiate("Gadgets/Apple", new Vector3(1, 1, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("Gadgets/Apple", new Vector3(1, 0, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("Gadgets/Fish", new Vector3(-1, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
