using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInteractComponent : InteractComponent
{
    public float eliminatingTime = 5f;
    public GameObject InteractAnchor;
    public GameObject ReleaseAnchor;
    
    Coroutine eliminatingCoroutine;

    bool isEliminating = false;
    PhotonView escapeePhotonView;

    private void OnEnable()
    {

    }

    protected override void OnInteract(GameObject hitObject)
    {
        var escapeeInteractComponent = hitObject.GetComponentInChildren<EscapeeInteractComponent>();
        
        if (escapeeInteractComponent != null)
        {
            escapeePhotonView = escapeeInteractComponent.gameObject.GetComponent<PhotonView>();

            escapeePhotonView.RPC("OnBeingInteractedByMonster", RpcTarget.All);

            eliminatingCoroutine = StartCoroutine(eliminatingProcess(escapeeInteractComponent));

            escapeePhotonView.RPC("onInteractBegin", RpcTarget.All, PhotonView.Get(this).ViewID);
        }
    }

    IEnumerator eliminatingProcess(EscapeeInteractComponent escapeeInteractComponent)
    {
        isEliminating = true;
        // animation
        escapeeInteractComponent.onBeingExecutedProcessBegin();

        yield return new WaitForSeconds(eliminatingTime);

        // Now the explayee has been killed.

        animator.SetBool("IsEating", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("IsEating", false);
        isEliminating = false;
    }

    public void onDisturbedFromExecuting(EscapeeInteractComponent escapeeInteractComponent)
    {
        if(isEliminating && eliminatingCoroutine != null)
        {
            StopCoroutine(eliminatingCoroutine);

            // animation

            escapeePhotonView.RPC("onBeingReleasedFromExecuting", RpcTarget.All, ReleaseAnchor.transform.position);

            Debug.Log("Being stopped from killing the escapee");

            isEliminating = false;
        }

    }



}
