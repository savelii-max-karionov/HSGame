using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSingleDeployer : Deployer
{
    public ToSingleDeployer(ITargetSelector targetSelector, List<IEffect> effects) : base(targetSelector, effects)
    {
    }

    public override void Deploy(GameObject producer)
    {
        throw new System.NotImplementedException();
    }
}
