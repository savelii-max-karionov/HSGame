using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationEventHandler : MonoBehaviour
{
    EscapeeInteractComponent escapeeBeingInteracted;
    public GameObject interactAnchor;
    public void onEscapeeBeingKilled()
    {
        if (PhotonView.Get(this).IsMine)
        {
            if (interactAnchor == null)
            {
                Debug.LogError("No anchor being set for animation event");
                return;
            }

            var objectInAnchor = interactAnchor.transform.GetChild(0);
            if (objectInAnchor == null)
            {
                Debug.LogError("No objected being interacted");
                return;
            }
            escapeeBeingInteracted = objectInAnchor.GetComponentInChildren<EscapeeInteractComponent>();
            if (escapeeBeingInteracted == null)
            {
                Debug.LogError("The object being interacted is not a escapee");
                return;
            }

            escapeeBeingInteracted.gameObject.GetComponent<PhotonView>().RPC("OnBeingkilledByMonster", RpcTarget.All);
        }
    }
}
