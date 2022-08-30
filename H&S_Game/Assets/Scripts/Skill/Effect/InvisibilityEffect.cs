using System.Collections;
using Photon.Pun;
using UnityEngine;


public class InvisibilityEffect : IEffect
{
    private float duration;
    
    public InvisibilityEffect(SkillData data)
    {
        duration = data.durationTime;
    }

    public void Start(GameObject producer, GameObject receiver = null)
    {
        if (receiver != null)
        {
            var photonView = receiver.GetComponent<PhotonView>();
            if (photonView)
            {
                photonView.RPC("makeTransparant", RpcTarget.All, producer.GetComponent<PlayerComponent>().Id, receiver.GetComponent<PlayerComponent>().Id,duration);
            }
        }
    }

}
