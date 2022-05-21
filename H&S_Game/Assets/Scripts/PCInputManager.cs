using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HS;

public class PCInputManager :InputManager
{
    private Vector3 mousePos;
    private Vector2 mousePos2D;

    public override float  horizontal
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public override float vertical { 
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    ////Start is called before the first frame update
    //void Start()
    //{

    //}

    ////Update is called once per frame
    //void Update()
    //{
    //    // This part can be improved. Input manager should not know any detail of HO.
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //converting mouse position to world points
    //        mousePos2D = new Vector2(mousePos.x, mousePos.y);

    //        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

    //        if (hit.collider != null)
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            hit.collider.gameObject.TryGetComponent<HidingObject>(out HidingObject hidingObject);
    //            if (hidingObject != null) hidingObject.Open();
    //        }
    //    }

    //}
}
