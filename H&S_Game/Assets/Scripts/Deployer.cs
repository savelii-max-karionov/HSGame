using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deployer 
{

    private ITargetSelector targetSelector;
    private List<IEffect> effects;

    public Deployer(ITargetSelector targetSelector, List<IEffect> effects)
    {
        this.targetSelector = targetSelector;
        this.effects = effects;
    }
    public abstract void Deploy(GameObject producer);
}
