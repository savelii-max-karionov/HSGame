using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deployer 
{

    protected ITargetSelector targetSelector;
    protected List<IEffect> effects;
    private bool canUseSkill = true;


    public Deployer(ITargetSelector targetSelector, List<IEffect> effects)
    {
        this.targetSelector = targetSelector;
        this.effects = effects;
    }

    public void deploy(GameObject producer, float cd)
    {
        if (canUseSkill)
        {
            Debug.Log("Begin to use skill");
            execute(producer);
            var skillManager = producer.GetComponent<SkillManager>();
            if(skillManager != null)
            {
                skillManager.StartCoroutine(countCoolDown(cd));
            }
        }
        else
        {
            Debug.Log("Cannot use skill right now!");
        }
    }
    public abstract void execute(GameObject producer);

    public IEnumerator countCoolDown(float cd)
    {
        canUseSkill = false;
        yield return new WaitForSeconds(cd);
        canUseSkill = true;
    }
}
