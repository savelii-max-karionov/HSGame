using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ClimbingTunnelingObject: MonoBehaviour{

    public enum FaceDirection
    {
        left,
        right
    };

    public enum HeightOptions
    {
        low,
        medium,
        high,
        downLow,
        downMedium,
        downHigh,
    }

    

    public FaceDirection directionToTrigger;
    PlayerMovement movement;
    InteractComponent interactComponent;
    public HeightOptions heightOption = HeightOptions.medium;

    HashSet<int> objsEnteredFromTriggerDir = new HashSet<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var colliderId = collision.gameObject.GetInstanceID();
        var escapeeComponet = collision.GetComponent<EscapeeComponent>();
        if (escapeeComponet && collision.GetInstanceID() == escapeeComponet.walkCollider.GetInstanceID())
        {
            Debug.Log(collision.GetInstanceID() + " detected enter");

            _updateDirSet(collision.GetInstanceID(), collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.GetInstanceID() + "detected leave, current:" + objsEnteredFromTriggerDir.ToString());

        // Only when the collider both enter and exit in expected direction can it trigger hopping.
        if (objsEnteredFromTriggerDir.Contains(collision.GetInstanceID())
             && getToColliderDir(collision.transform.position) == directionToTrigger)
        {
            EscapeeInteractComponent escapeeInteractComponent = collision.GetComponentInChildren<EscapeeInteractComponent>();
            if (escapeeInteractComponent)
            {
                // trigger hopping
                Debug.Log("ClimbingTunnelingObject: I am hopping!");

                letEscapeeHop(escapeeInteractComponent, heightOption);
            }
            objsEnteredFromTriggerDir.Remove(collision.gameObject.GetInstanceID());
        }
    }

    void letEscapeeHop(EscapeeInteractComponent interactComponent, HeightOptions option)
    {
        interactComponent.hop(option, directionToTrigger);
    }

    // return the enum of the direction from the object's position to the collider's.
    FaceDirection getToColliderDir(Vector3 colliderPos)
    {
        Debug.Log("the direction is :" + ((colliderPos.x - transform.position.x) > 0 ? FaceDirection.right : FaceDirection.left).ToString());
        return (colliderPos.x - transform.position.x) > 0 ? FaceDirection.right : FaceDirection.left;
    }

    
    void _updateDirSet(int instanceId, Vector3 colliderPos)
    {
        // Only record objects from correct direction.
        //if (getToColliderDir(colliderPos) != directionToTrigger)
        //{
        //    objsEnteredFromTriggerDir.Add(instanceId);
        //}

        objsEnteredFromTriggerDir.Add(instanceId);
    }

    
}
