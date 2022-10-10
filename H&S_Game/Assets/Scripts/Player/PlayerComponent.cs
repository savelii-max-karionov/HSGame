using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private bool isHiding = false;
    private string id;

    public bool IsHiding { get => isHiding; set => isHiding = value; }
    public string Id {
        get {
            if (id == null)
            {
                id = GetComponent<PhotonView>()?.ViewID.ToString();
            }
            return id;
        }
    }

    protected void OnEnable()
    {
        GameStatistics.playerList.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        GameStatistics.playerList.Remove(gameObject);
    }
}
