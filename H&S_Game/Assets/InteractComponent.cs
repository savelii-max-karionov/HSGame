using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    public Collider2D interactCollider;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Collider2D[] colliders = new Collider2D[10];
        if (Input.GetMouseButtonDown(0)&&interactCollider.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition)))
        {
     
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.transform != null)
            {
                var interactObj = rayHit.transform.gameObject.GetComponent<InteractableObject>();
                if (interactObj != null)
                {
                    interactObj.mouseUp();
                }
            }



        }
    }
}
