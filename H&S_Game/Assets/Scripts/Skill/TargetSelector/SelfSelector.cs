using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSelector : ITargetSelector
{
    public List<GameObject> GetTargets()
    {
        var targets = new List<GameObject>();
        targets.Add(GameStatistics.findControledPlayer());
        return targets;
    }
}
