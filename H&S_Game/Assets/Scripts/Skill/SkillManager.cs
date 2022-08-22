using System.Collections;
using System.Collections.Generic;
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

         deployers[index].Deploy(gameObject);
    }


    
}
