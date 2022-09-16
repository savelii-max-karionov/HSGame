using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingTunnelingObject : TunnelingObject
{
    public bool isClimbingUp;
    PlayerMovement movement;
    InteractComponent interactComponent;

    protected override void preTunneling()
    {
        disableVisualWhileTunneling = false;

        base.preTunneling();
        movement = playerObject.GetComponent<PlayerMovement>();
        if (!movement){
            return;
        }

        if (isClimbingUp)
        {
            movement.changeClimbingState(true);
        }

        interactComponent = playerObject.GetComponentInChildren<InteractComponent>();

        if (!interactComponent) {
            return;
        }

        if (!isClimbingUp)
        {
            interactComponent.OnTransportEnd += handleTransportEnd;
        }
    }
    
    protected override void postInvokeTunneling()
    {

    }

    private void handleTransportEnd()
    {
        if (movement && !isClimbingUp)
        {
            movement.changeClimbingState(false);
        }

        interactComponent.OnTransportEnd -= handleTransportEnd;
    }
}
