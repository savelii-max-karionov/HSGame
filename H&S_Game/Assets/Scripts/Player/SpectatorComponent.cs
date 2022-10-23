using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorComponent : PlayerComponent
{
    private new void OnEnable()
    {
        base.OnEnable();

        // TODO: use a scriptable object to config visibility.
        var controledPlayer = GameStatistics.findControledPlayer();

        if (controledPlayer)
        {
            if (controledPlayer.CompareTag("Spectator"))
            {
                visualObject.SetActive(true);
                //GameStatistics.onlyEnableVisualOfTags(new List<string>{"Spectator"});
            }
            else
            {
                visualObject.SetActive(false);
            }
        }


    }
}
