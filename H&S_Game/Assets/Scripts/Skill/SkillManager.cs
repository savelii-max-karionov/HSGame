using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillData> skillDataList = new List<SkillData>();
    public int maxSkillNum = 1;

    private List<Deployer> deployers = new List<Deployer>();

    private void OnEnable()
    {
        InitializeSkills();
    }

    public void InitializeSkills()
    {
        for(int i = 0; i < skillDataList.Count; i++)
        {
            if(i < maxSkillNum)
            {
                deployers.Add(DeployerFactory.createDeployer(skillDataList[i]));
            }
            else
            {
                Debug.LogWarning("Skill Data exceeds the maxSkillNum");
                return;
            }
        }
    }

    public void triggerSkill(int index)
    {
        if (index >= skillDataList.Count)
        {
            Debug.LogError("Skill index out of range! index: "+ index);
            return;
        }

         deployers[index].deploy(gameObject,skillDataList[index].coolDown);
    }

    [PunRPC]
    private void makeTransparant(string producer_id, string receiver_id, float duration)
    {
        var producer = GameStatistics.playerList.Find(e => e.GetComponent<PlayerComponent>().Id == producer_id);
        var receiver = GameStatistics.playerList.Find(e => e.GetComponent<PlayerComponent>().Id == receiver_id);
        var renderer = receiver.GetComponentInChildren<SpriteRenderer>();
        var photonView = receiver.GetPhotonView();
        if (renderer && photonView)
        {
            var manager = producer.GetComponent<SkillManager>();
            if (photonView.IsMine)
            {
                manager.StartCoroutine(changeAlphaProgressively(renderer, -0.5f, 0.3f, 1f));
            }
            else
            {
                manager.StartCoroutine(changeAlphaProgressively(renderer, -0.5f, 0.0f, 1f));
            }
            if (manager != null)
            {
                manager.StartCoroutine(resetAlpha(renderer, duration));
            }

        }
        else
        {
            Debug.Log("Invisible effect of " + receiver.name + " failed because the SpriteRenderer/PhotonView cannot be found, the producer is " + producer.name);
        }
    }

    private IEnumerator resetAlpha(SpriteRenderer renderer, float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Begin to reset Aplha");
        yield return StartCoroutine(changeAlphaProgressively(renderer, 0.5f, 0.0f, 1.0f));
    }
    private IEnumerator changeAlphaProgressively(SpriteRenderer renderer, float changingRate, float min, float max)
    {
        while ((renderer.color.a < max && changingRate > 0) || (renderer.color.a > min && changingRate < 0))
        {
            Debug.Log("1111");
            renderer.color = new Color(1f, 1f, 1f, Mathf.Clamp(changingRate * Time.deltaTime + renderer.color.a, min, max));
            yield return 0;
        }
        Debug.Log("end");
    }
}
