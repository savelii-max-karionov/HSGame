using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSingleDeployer : Deployer
{
    public ToSingleDeployer(ITargetSelector targetSelector, List<IEffect> effects) : base(targetSelector, effects)
    {

    }

    public override void execute(GameObject producer)
    {

        var targets = targetSelector.GetTargets();
        if(targets.Count != 0)
        {
            foreach(var effect in effects)
            {
                effect.Start(producer, targets[0]);
            }
        }



    }
}
