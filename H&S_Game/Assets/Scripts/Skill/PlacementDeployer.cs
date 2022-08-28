using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementDeployer : Deployer
{

    protected GameObject prefab;
    public PlacementDeployer(ITargetSelector targetSelector, List<IEffect> effects, GameObject prefab) : base(targetSelector, effects)
    {
        this.prefab = prefab;
    }

    public override void execute(GameObject producer)
    {
        Debug.Log("test OkacenebtDeployer");
    }
}
