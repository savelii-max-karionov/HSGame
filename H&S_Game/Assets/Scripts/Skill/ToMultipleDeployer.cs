using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMultipleDeployer : Deployer
{
    public ToMultipleDeployer(ITargetSelector targetSelector, List<IEffect> effects) : base(targetSelector, effects)
    {
    }

    public override void execute(GameObject producer)
    {
        throw new System.NotImplementedException();
    }
}
